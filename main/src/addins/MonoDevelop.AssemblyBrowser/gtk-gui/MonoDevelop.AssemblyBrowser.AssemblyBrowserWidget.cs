// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.AssemblyBrowser {
    
    
    public partial class AssemblyBrowserWidget {
        
        private Gtk.HPaned hpaned1;
        
        private Gtk.VPaned vpaned1;
        
        private Gtk.ScrolledWindow scrolledwindow2;
        
        private Gtk.ScrolledWindow GtkScrolledWindow;
        
        private Gtk.Label descriptionLabel;
        
        private Gtk.Notebook notebook1;
        
        private Gtk.ScrolledWindow documentationScrolledWindow;
        
        private Gtk.Label documentationLabel;
        
        private Gtk.Label label1;
        
        private Gtk.ScrolledWindow disassemblerScrolledWindow;
        
        private Gtk.Label disassemblerLabel;
        
        private Gtk.ScrolledWindow decompilerScrolledWindow;
        
        private Gtk.Label decompilerLabel;
        
        private Gtk.VBox searchWidget;
        
        private Gtk.HBox hbox1;
        
        private Gtk.Label label4;
        
        private Gtk.Entry searchEntry;
        
        private Gtk.Label label5;
        
        private Gtk.ComboBox searchInCombobox;
        
        private Gtk.ScrolledWindow scrolledwindow1;
        
        private Gtk.TreeView searchTreeview;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget MonoDevelop.AssemblyBrowser.AssemblyBrowserWidget
            Stetic.BinContainer.Attach(this);
            this.Name = "MonoDevelop.AssemblyBrowser.AssemblyBrowserWidget";
            // Container child MonoDevelop.AssemblyBrowser.AssemblyBrowserWidget.Gtk.Container+ContainerChild
            this.hpaned1 = new Gtk.HPaned();
            this.hpaned1.CanFocus = true;
            this.hpaned1.Name = "hpaned1";
            this.hpaned1.Position = 185;
            // Container child hpaned1.Gtk.Paned+PanedChild
            this.vpaned1 = new Gtk.VPaned();
            this.vpaned1.CanFocus = true;
            this.vpaned1.Name = "vpaned1";
            this.vpaned1.Position = 367;
            // Container child vpaned1.Gtk.Paned+PanedChild
            this.scrolledwindow2 = new Gtk.ScrolledWindow();
            this.scrolledwindow2.CanFocus = true;
            this.scrolledwindow2.Name = "scrolledwindow2";
            this.scrolledwindow2.ShadowType = ((Gtk.ShadowType)(1));
            this.vpaned1.Add(this.scrolledwindow2);
            Gtk.Paned.PanedChild w1 = ((Gtk.Paned.PanedChild)(this.vpaned1[this.scrolledwindow2]));
            w1.Resize = false;
            // Container child vpaned1.Gtk.Paned+PanedChild
            this.GtkScrolledWindow = new Gtk.ScrolledWindow();
            this.GtkScrolledWindow.Name = "GtkScrolledWindow";
            this.GtkScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child GtkScrolledWindow.Gtk.Container+ContainerChild
            Gtk.Viewport w2 = new Gtk.Viewport();
            w2.ShadowType = ((Gtk.ShadowType)(0));
            // Container child GtkViewport2.Gtk.Container+ContainerChild
            this.descriptionLabel = new Gtk.Label();
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Xpad = 6;
            this.descriptionLabel.Ypad = 6;
            this.descriptionLabel.Xalign = 0F;
            this.descriptionLabel.Yalign = 0F;
            this.descriptionLabel.LabelProp = "";
            this.descriptionLabel.UseMarkup = true;
            this.descriptionLabel.Selectable = true;
            w2.Add(this.descriptionLabel);
            this.GtkScrolledWindow.Add(w2);
            this.vpaned1.Add(this.GtkScrolledWindow);
            this.hpaned1.Add(this.vpaned1);
            Gtk.Paned.PanedChild w6 = ((Gtk.Paned.PanedChild)(this.hpaned1[this.vpaned1]));
            w6.Resize = false;
            // Container child hpaned1.Gtk.Paned+PanedChild
            this.notebook1 = new Gtk.Notebook();
            this.notebook1.CanFocus = true;
            this.notebook1.Name = "notebook1";
            this.notebook1.CurrentPage = 0;
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.documentationScrolledWindow = new Gtk.ScrolledWindow();
            this.documentationScrolledWindow.Name = "documentationScrolledWindow";
            this.documentationScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child documentationScrolledWindow.Gtk.Container+ContainerChild
            Gtk.Viewport w7 = new Gtk.Viewport();
            w7.ShadowType = ((Gtk.ShadowType)(0));
            // Container child GtkViewport.Gtk.Container+ContainerChild
            this.documentationLabel = new Gtk.Label();
            this.documentationLabel.Name = "documentationLabel";
            this.documentationLabel.Xpad = 6;
            this.documentationLabel.Ypad = 6;
            this.documentationLabel.Xalign = 0F;
            this.documentationLabel.Yalign = 0F;
            this.documentationLabel.LabelProp = "";
            this.documentationLabel.UseMarkup = true;
            this.documentationLabel.Selectable = true;
            w7.Add(this.documentationLabel);
            this.documentationScrolledWindow.Add(w7);
            this.notebook1.Add(this.documentationScrolledWindow);
            // Notebook tab
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("page4");
            this.notebook1.SetTabLabel(this.documentationScrolledWindow, this.label1);
            this.label1.ShowAll();
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.disassemblerScrolledWindow = new Gtk.ScrolledWindow();
            this.disassemblerScrolledWindow.Name = "disassemblerScrolledWindow";
            this.disassemblerScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child disassemblerScrolledWindow.Gtk.Container+ContainerChild
            Gtk.Viewport w11 = new Gtk.Viewport();
            w11.ShadowType = ((Gtk.ShadowType)(0));
            // Container child GtkViewport3.Gtk.Container+ContainerChild
            this.disassemblerLabel = new Gtk.Label();
            this.disassemblerLabel.Name = "disassemblerLabel";
            this.disassemblerLabel.Xpad = 6;
            this.disassemblerLabel.Ypad = 6;
            this.disassemblerLabel.Xalign = 0F;
            this.disassemblerLabel.Yalign = 0F;
            this.disassemblerLabel.LabelProp = "";
            this.disassemblerLabel.UseMarkup = true;
            this.disassemblerLabel.Selectable = true;
            w11.Add(this.disassemblerLabel);
            this.disassemblerScrolledWindow.Add(w11);
            this.notebook1.Add(this.disassemblerScrolledWindow);
            Gtk.Notebook.NotebookChild w14 = ((Gtk.Notebook.NotebookChild)(this.notebook1[this.disassemblerScrolledWindow]));
            w14.Position = 1;
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.decompilerScrolledWindow = new Gtk.ScrolledWindow();
            this.decompilerScrolledWindow.Name = "decompilerScrolledWindow";
            this.decompilerScrolledWindow.ShadowType = ((Gtk.ShadowType)(1));
            // Container child decompilerScrolledWindow.Gtk.Container+ContainerChild
            Gtk.Viewport w15 = new Gtk.Viewport();
            w15.ShadowType = ((Gtk.ShadowType)(0));
            // Container child GtkViewport4.Gtk.Container+ContainerChild
            this.decompilerLabel = new Gtk.Label();
            this.decompilerLabel.Name = "decompilerLabel";
            this.decompilerLabel.Xpad = 6;
            this.decompilerLabel.Ypad = 6;
            this.decompilerLabel.Xalign = 0F;
            this.decompilerLabel.Yalign = 0F;
            this.decompilerLabel.LabelProp = "";
            this.decompilerLabel.UseMarkup = true;
            this.decompilerLabel.Selectable = true;
            w15.Add(this.decompilerLabel);
            this.decompilerScrolledWindow.Add(w15);
            this.notebook1.Add(this.decompilerScrolledWindow);
            Gtk.Notebook.NotebookChild w18 = ((Gtk.Notebook.NotebookChild)(this.notebook1[this.decompilerScrolledWindow]));
            w18.Position = 2;
            // Container child notebook1.Gtk.Notebook+NotebookChild
            this.searchWidget = new Gtk.VBox();
            this.searchWidget.Name = "searchWidget";
            this.searchWidget.Spacing = 6;
            // Container child searchWidget.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("_Search:");
            this.label4.UseUnderline = true;
            this.hbox1.Add(this.label4);
            Gtk.Box.BoxChild w19 = ((Gtk.Box.BoxChild)(this.hbox1[this.label4]));
            w19.Position = 0;
            w19.Expand = false;
            w19.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.searchEntry = new Gtk.Entry();
            this.searchEntry.CanFocus = true;
            this.searchEntry.Name = "searchEntry";
            this.searchEntry.IsEditable = true;
            this.searchEntry.InvisibleChar = '●';
            this.hbox1.Add(this.searchEntry);
            Gtk.Box.BoxChild w20 = ((Gtk.Box.BoxChild)(this.hbox1[this.searchEntry]));
            w20.Position = 1;
            // Container child hbox1.Gtk.Box+BoxChild
            this.label5 = new Gtk.Label();
            this.label5.Name = "label5";
            this.label5.LabelProp = Mono.Unix.Catalog.GetString("_in:");
            this.label5.UseUnderline = true;
            this.hbox1.Add(this.label5);
            Gtk.Box.BoxChild w21 = ((Gtk.Box.BoxChild)(this.hbox1[this.label5]));
            w21.Position = 2;
            w21.Expand = false;
            w21.Fill = false;
            // Container child hbox1.Gtk.Box+BoxChild
            this.searchInCombobox = Gtk.ComboBox.NewText();
            this.searchInCombobox.Name = "searchInCombobox";
            this.hbox1.Add(this.searchInCombobox);
            Gtk.Box.BoxChild w22 = ((Gtk.Box.BoxChild)(this.hbox1[this.searchInCombobox]));
            w22.Position = 3;
            w22.Expand = false;
            w22.Fill = false;
            this.searchWidget.Add(this.hbox1);
            Gtk.Box.BoxChild w23 = ((Gtk.Box.BoxChild)(this.searchWidget[this.hbox1]));
            w23.Position = 0;
            w23.Expand = false;
            w23.Fill = false;
            // Container child searchWidget.Gtk.Box+BoxChild
            this.scrolledwindow1 = new Gtk.ScrolledWindow();
            this.scrolledwindow1.CanFocus = true;
            this.scrolledwindow1.Name = "scrolledwindow1";
            this.scrolledwindow1.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow1.Gtk.Container+ContainerChild
            this.searchTreeview = new Gtk.TreeView();
            this.searchTreeview.CanFocus = true;
            this.searchTreeview.Name = "searchTreeview";
            this.searchTreeview.HeadersClickable = true;
            this.scrolledwindow1.Add(this.searchTreeview);
            this.searchWidget.Add(this.scrolledwindow1);
            Gtk.Box.BoxChild w25 = ((Gtk.Box.BoxChild)(this.searchWidget[this.scrolledwindow1]));
            w25.Position = 1;
            this.notebook1.Add(this.searchWidget);
            Gtk.Notebook.NotebookChild w26 = ((Gtk.Notebook.NotebookChild)(this.notebook1[this.searchWidget]));
            w26.Position = 3;
            this.hpaned1.Add(this.notebook1);
            this.Add(this.hpaned1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.label4.MnemonicWidget = this.searchEntry;
            this.label5.MnemonicWidget = this.searchInCombobox;
            this.Show();
        }
    }
}
