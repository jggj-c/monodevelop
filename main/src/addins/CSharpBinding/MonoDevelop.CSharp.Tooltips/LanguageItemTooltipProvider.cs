// LanguageItemTooltipProvider.cs
//
// Author:
//   Lluis Sanchez Gual <lluis@novell.com>
//
// Copyright (c) 2008 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

using System;
using Mono.TextEditor;
using MonoDevelop.Ide.TypeSystem;
using ICSharpCode.NRefactory.Semantics;
using MonoDevelop.Ide;
using ICSharpCode.NRefactory.CSharp;
using MonoDevelop.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Gtk;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using System.Threading;
using System.Text;
using MonoDevelop.Core;
using System.Collections.Generic;
using System.Linq;
using MonoDevelop.CSharp.Resolver;
using MonoDevelop.Ide.CodeCompletion;
using MonoDevelop.CSharp.Completion;
using MonoDevelop.Components;

namespace MonoDevelop.SourceEditor
{
	public class LanguageItemTooltipProvider: TooltipProvider
	{
		public LanguageItemTooltipProvider()
		{
		}

		class ToolTipData
		{
			public SyntaxTree Unit;
			public ResolveResult Result;
			public AstNode Node;
			public CSharpAstResolver Resolver;

			public ToolTipData (ICSharpCode.NRefactory.CSharp.SyntaxTree unit, ICSharpCode.NRefactory.Semantics.ResolveResult result, ICSharpCode.NRefactory.CSharp.AstNode node, CSharpAstResolver file)
			{
				this.Unit = unit;
				this.Result = result;
				this.Node = node;
				this.Resolver = file;
			}
		}

		#region ITooltipProvider implementation 
		
		public override TooltipItem GetItem (Mono.TextEditor.TextEditor editor, int offset)
		{
			var doc = IdeApp.Workbench.ActiveDocument;
			if (doc == null || doc.ParsedDocument == null)
				return null;
			var unit = doc.ParsedDocument.GetAst<SyntaxTree> ();
			if (unit == null)
				return null;

			var file = doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile;
			if (file == null)
				return null;
			
			ResolveResult result;
			AstNode node;
			var loc = editor.OffsetToLocation (offset);
			if (!doc.TryResolveAt (loc, out result, out node)) {
				if (node is CSharpTokenNode)
					return new TooltipItem (new ToolTipData (unit, result, node, null), offset, 0);
				return null;
			}
			var resolver = new CSharpAstResolver (doc.Compilation, unit, file);
			resolver.ApplyNavigator (new NodeListResolveVisitorNavigator (node), CancellationToken.None);

			int startOffset = offset;
			int endOffset = offset;
			return new TooltipItem (new ToolTipData (unit, result, node, resolver), startOffset, endOffset - startOffset);
		}
		
		ResolveResult lastResult = null;
		TooltipInformationWindow lastWindow = null;

		protected override Gtk.Window CreateTooltipWindow (Mono.TextEditor.TextEditor editor, int offset, Gdk.ModifierType modifierState, TooltipItem item)
		{
			var doc = IdeApp.Workbench.ActiveDocument;
			if (doc == null)
				return null;

			var titem = (ToolTipData)item.Item;

			if (lastResult != null && lastWindow.IsRealized && 
				titem.Result != null && lastResult.Type.Equals (titem.Result.Type))
				return lastWindow;
			var tooltipInformation = CreateTooltip (titem, offset, null);
			if (tooltipInformation == null || string.IsNullOrEmpty (tooltipInformation.SignatureMarkup))
				return null;

			var result = new TooltipInformationWindow ();

			result.ShowArrow = true;
			result.AddOverload (tooltipInformation);
			result.RepositionWindow ();
			if (lastWindow != null && lastWindow.Visible)
				lastWindow.Destroy ();
			lastWindow = result;
			lastResult = titem.Result;
			return result;
		}

		public override Gtk.Window ShowTooltipWindow (TextEditor editor, int offset, Gdk.ModifierType modifierState, int mouseX, int mouseY, TooltipItem item)
		{
			var tipWindow = CreateTooltipWindow (editor, offset, modifierState, item) as TooltipInformationWindow;
			if (tipWindow == null)
				return null;
			/*
			int ox = 0, oy = 0;
			if (editor.GdkWindow != null)
				editor.GdkWindow.GetOrigin (out ox, out oy);*/
			var titem = (ToolTipData)item.Item;
			var p1 = editor.LocationToPoint (titem.Node.StartLocation);
			var p2 = editor.LocationToPoint (titem.Node.EndLocation);
			var caret = new Gdk.Rectangle ((int)p1.X - editor.Allocation.X, (int)p2.Y - editor.Allocation.Y, (int)(p2.X - p1.X), (int)editor.LineHeight);

			tipWindow.ShowPopup (editor, caret, PopupPosition.Top);
			
			return tipWindow;
		}


		TooltipInformation CreateTooltip (ToolTipData data, int offset, Ambience ambience)
		{
			ResolveResult result = data.Result;
			var doc = IdeApp.Workbench.ActiveDocument;
			if (doc == null)
				return null;

			if (result == null && data.Node is CSharpTokenNode) {
				var tooltipInfo = new TooltipInformation ();
				var resolver = (doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile).GetResolver (doc.Compilation, doc.Editor.Caret.Location);
				var sig = new SignatureMarkupCreator (resolver, doc.GetFormattingPolicy ().CreateOptions ());
				sig.BreakLineAfterReturnType = false;
				tooltipInfo.SignatureMarkup = sig.GetKeywordMarkup (data.Node.GetText ());;
				return tooltipInfo;
			}

			if (result is UnknownIdentifierResolveResult) {
				return new TooltipInformation () {
					SignatureMarkup = string.Format ("error CS0103: The name `{0}' does not exist in the current context", ((UnknownIdentifierResolveResult)result).Identifier)
				};
			} else if (result is UnknownMemberResolveResult) {
				var ur = (UnknownMemberResolveResult)result;
				if (ur.TargetType.Kind != TypeKind.Unknown) {
					return new TooltipInformation () {
						SignatureMarkup = string.Format ("error CS0117: `{0}' does not contain a definition for `{1}'", ur.TargetType.FullName, ur.MemberName)
					};
				}
			} else if (result.IsError) {
				return new TooltipInformation () {
					SignatureMarkup = "Unknown resolve error."
				};
			}

			if (result is LocalResolveResult) {
				var lr = (LocalResolveResult)result;
				var tooltipInfo = new TooltipInformation ();
				var resolver = (doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile).GetResolver (doc.Compilation, doc.Editor.Caret.Location);
				var sig = new SignatureMarkupCreator (resolver, doc.GetFormattingPolicy ().CreateOptions ());
				sig.BreakLineAfterReturnType =  false;
				tooltipInfo.SignatureMarkup = sig.GetLocalVariableMarkup (lr.Variable);
				return tooltipInfo;
			} else if (result is MethodGroupResolveResult) {
				var mrr = (MethodGroupResolveResult)result;
				var allMethods = new List<IMethod> (mrr.Methods);
				foreach (var l in mrr.GetExtensionMethods ()) {
					allMethods.AddRange (l);
				}
				
				var method = allMethods.FirstOrDefault ();
				if (method != null) {
					return MemberCompletionData.CreateTooltipInformation (
						doc.Compilation,
						doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile,
						doc.Editor,
						doc.GetFormattingPolicy (),
						method, 
						false);
				}
			} else if (result is MemberResolveResult) {
				var member = ((MemberResolveResult)result).Member;
				return MemberCompletionData.CreateTooltipInformation (
					doc.Compilation,
					doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile,
					doc.Editor,
					doc.GetFormattingPolicy (),
					member, 
					false);
			} else if (result is NamespaceResolveResult) {
				var tooltipInfo = new TooltipInformation ();
				var resolver = (doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile).GetResolver (doc.Compilation, doc.Editor.Caret.Location);
				var sig = new SignatureMarkupCreator (resolver, doc.GetFormattingPolicy ().CreateOptions ());
				sig.BreakLineAfterReturnType =  false;
				tooltipInfo.SignatureMarkup = sig.GetMarkup (((NamespaceResolveResult)result).Namespace);
				return tooltipInfo;
			} else {
				if (result.Type.GetDefinition () != null) {
					return MemberCompletionData.CreateTooltipInformation (
						doc.Compilation,
						doc.ParsedDocument.ParsedFile as CSharpUnresolvedFile,
						doc.Editor,
						doc.GetFormattingPolicy (),
						result.Type.GetDefinition (), 
						false);
				}
			}

			return null;
		}
		class ErrorVisitor : DepthFirstAstVisitor
		{
			readonly CSharpAstResolver resolver;
			readonly CancellationToken cancellationToken;

			ResolveResult errorResolveResult;
			public ResolveResult ErrorResolveResult {
				get {
					return errorResolveResult;
				}
			}			

			AstNode errorNode;

			public AstNode ErrorNode {
				get {
					return errorNode;
				}
			}

			public ErrorVisitor (CSharpAstResolver resolver, CancellationToken cancellationToken = default(CancellationToken))
			{
				this.resolver = resolver;
				this.cancellationToken = cancellationToken;
			}
			
			protected override void VisitChildren (AstNode node)
			{
				if (ErrorResolveResult != null || cancellationToken.IsCancellationRequested)
					return;
				if (node is Expression) {
					var rr = resolver.Resolve (node, cancellationToken);
					if (rr.IsError) {
						errorResolveResult = rr;
						errorNode = node;
					}
				}
				base.VisitChildren (node);
			}
		}

		
		protected override void GetRequiredPosition (Mono.TextEditor.TextEditor editor, Gtk.Window tipWindow, out int requiredWidth, out double xalign)
		{
			var win = (TooltipInformationWindow) tipWindow;
			requiredWidth = win.Allocation.Width;
			xalign = 0.5;
		}

		#endregion 

	}
}
