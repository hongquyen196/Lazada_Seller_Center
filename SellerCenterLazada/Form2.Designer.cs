using SellerCenterLazada.Controls;

namespace SellerCenterLazada
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.styleManager2 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.gridAccount = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.userLoginBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnDocFile = new DevComponents.DotNetBar.ButtonX();
            this.btnDangNhap = new DevComponents.DotNetBar.ButtonX();
            this.btnThem = new DevComponents.DotNetBar.ButtonX();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabManager = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.btnHenGio = new DevComponents.DotNetBar.ButtonX();
            this.btnDangDao = new DevComponents.DotNetBar.ButtonX();
            this.sideNav1 = new DevComponents.DotNetBar.Controls.SideNav();
            this.sideNavPanel1 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.productInfo = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.gridColumn20 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn19 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.productInfoVoListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sideNavPanel2 = new DevComponents.DotNetBar.Controls.SideNavPanel();
            this.sideNavItem1 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.separator1 = new DevComponents.DotNetBar.Separator();
            this.sideNavItem2 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.separator2 = new DevComponents.DotNetBar.Separator();
            this.sideNavItem3 = new DevComponents.DotNetBar.Controls.SideNavItem();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.userLoginBindingSource)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabManager)).BeginInit();
            this.tabManager.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.sideNav1.SuspendLayout();
            this.sideNavPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productInfoVoListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.ribbonControl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl1.CanCustomize = false;
            this.ribbonControl1.CaptionVisible = true;
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonControl1.ForeColor = System.Drawing.Color.Black;
            this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.ribbonControl1.Location = new System.Drawing.Point(5, 1);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.ribbonControl1.Size = new System.Drawing.Size(902, 672);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl1.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl1.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl1.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabIndex = 0;
            this.ribbonControl1.Text = "Lazada Seller";
            // 
            // styleManager2
            // 
            this.styleManager2.ManagerStyle = DevComponents.DotNetBar.eStyle.VisualStudio2010Blue;
            this.styleManager2.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199))))));
            // 
            // gridAccount
            // 
            this.gridAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAccount.BackColor = System.Drawing.Color.White;
            this.gridAccount.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.gridAccount.ForeColor = System.Drawing.Color.Black;
            this.gridAccount.Location = new System.Drawing.Point(8, 30);
            this.gridAccount.Name = "gridAccount";
            // 
            // 
            // 
            this.gridAccount.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.gridAccount.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.gridAccount.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.gridAccount.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.gridAccount.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.gridAccount.PrimaryGrid.DataSource = this.userLoginBindingSource;
            this.gridAccount.PrimaryGrid.MultiSelect = false;
            this.gridAccount.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.gridAccount.Size = new System.Drawing.Size(899, 134);
            this.gridAccount.TabIndex = 1;
            this.gridAccount.Text = "gridAccount";
            this.gridAccount.RowDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs>(this.gridAccount_RowDoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AllowEdit = false;
            this.gridColumn1.DataPropertyName = "username";
            this.gridColumn1.Name = "Tài khoản";
            this.gridColumn1.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AllowEdit = false;
            this.gridColumn2.DataPropertyName = "password";
            this.gridColumn2.Name = "Mật khẩu";
            // 
            // gridColumn3
            // 
            this.gridColumn3.AllowEdit = false;
            this.gridColumn3.DataPropertyName = "name";
            this.gridColumn3.Name = "Tên";
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AllowEdit = false;
            this.gridColumn4.DataPropertyName = "cookie";
            this.gridColumn4.Name = "Cookie";
            this.gridColumn4.Width = 200;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AllowEdit = false;
            this.gridColumn5.DataPropertyName = "status";
            this.gridColumn5.Name = "Trạng thái";
            // 
            // userLoginBindingSource
            // 
            this.userLoginBindingSource.DataSource = typeof(SellerCenterLazada.Models.UserLogin);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.White;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2013;
            this.groupPanel1.Controls.Add(this.btnDocFile);
            this.groupPanel1.Controls.Add(this.btnDangNhap);
            this.groupPanel1.Controls.Add(this.btnThem);
            this.groupPanel1.Controls.Add(this.txtPassword);
            this.groupPanel1.Controls.Add(this.label2);
            this.groupPanel1.Controls.Add(this.txtAccount);
            this.groupPanel1.Controls.Add(this.label1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(8, 170);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(899, 81);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 2;
            this.groupPanel1.Text = "Cài đặt tài khoản";
            // 
            // btnDocFile
            // 
            this.btnDocFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDocFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDocFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDocFile.Location = new System.Drawing.Point(751, 17);
            this.btnDocFile.Name = "btnDocFile";
            this.btnDocFile.Size = new System.Drawing.Size(75, 23);
            this.btnDocFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDocFile.TabIndex = 4;
            this.btnDocFile.Text = "Đọc file";
            this.btnDocFile.Click += new System.EventHandler(this.btnDocFile_Click);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDangNhap.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDangNhap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDangNhap.Location = new System.Drawing.Point(647, 17);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(75, 23);
            this.btnDangNhap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnThem
            // 
            this.btnThem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnThem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThem.Location = new System.Drawing.Point(544, 17);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPassword.Location = new System.Drawing.Point(374, 18);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(146, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(310, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật khẩu:";
            // 
            // txtAccount
            // 
            this.txtAccount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtAccount.Location = new System.Drawing.Point(128, 18);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(146, 20);
            this.txtAccount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(64, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tài khoản:";
            // 
            // tabManager
            // 
            this.tabManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.tabManager.CanReorderTabs = true;
            this.tabManager.Controls.Add(this.tabControlPanel1);
            this.tabManager.Controls.Add(this.tabControlPanel2);
            this.tabManager.Enabled = false;
            this.tabManager.ForeColor = System.Drawing.Color.Black;
            this.tabManager.Location = new System.Drawing.Point(8, 257);
            this.tabManager.Name = "tabManager";
            this.tabManager.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabManager.SelectedTabIndex = 0;
            this.tabManager.Size = new System.Drawing.Size(896, 410);
            this.tabManager.TabIndex = 3;
            this.tabManager.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabManager.Tabs.Add(this.tabItem1);
            this.tabManager.Tabs.Add(this.tabItem2);
            this.tabManager.Text = "tabManager";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.sideNav1);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(896, 384);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // btnHenGio
            // 
            this.btnHenGio.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHenGio.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHenGio.Location = new System.Drawing.Point(110, 3);
            this.btnHenGio.Name = "btnHenGio";
            this.btnHenGio.Size = new System.Drawing.Size(75, 23);
            this.btnHenGio.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHenGio.TabIndex = 1;
            this.btnHenGio.Text = "Hẹn giờ";
            this.btnHenGio.Click += new System.EventHandler(this.btnHenGio_Click);
            // 
            // btnDangDao
            // 
            this.btnDangDao.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDangDao.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDangDao.Location = new System.Drawing.Point(3, 3);
            this.btnDangDao.Name = "btnDangDao";
            this.btnDangDao.Size = new System.Drawing.Size(75, 23);
            this.btnDangDao.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDangDao.TabIndex = 1;
            this.btnDangDao.Text = "Đăng dạo";
            // 
            // sideNav1
            // 
            this.sideNav1.Controls.Add(this.sideNavPanel1);
            this.sideNav1.Controls.Add(this.sideNavPanel2);
            this.sideNav1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNav1.EnableClose = false;
            this.sideNav1.EnableMaximize = false;
            this.sideNav1.EnableSplitter = false;
            this.sideNav1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sideNavItem1,
            this.separator1,
            this.sideNavItem2,
            this.separator2,
            this.sideNavItem3});
            this.sideNav1.Location = new System.Drawing.Point(1, 1);
            this.sideNav1.Name = "sideNav1";
            this.sideNav1.Padding = new System.Windows.Forms.Padding(1);
            this.sideNav1.Size = new System.Drawing.Size(894, 382);
            this.sideNav1.TabIndex = 0;
            this.sideNav1.Text = "sideNav1";
            // 
            // sideNavPanel1
            // 
            this.sideNavPanel1.Controls.Add(this.btnHenGio);
            this.sideNavPanel1.Controls.Add(this.richTextBox1);
            this.sideNavPanel1.Controls.Add(this.btnDangDao);
            this.sideNavPanel1.Controls.Add(this.productInfo);
            this.sideNavPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideNavPanel1.Location = new System.Drawing.Point(142, 31);
            this.sideNavPanel1.Name = "sideNavPanel1";
            this.sideNavPanel1.Size = new System.Drawing.Size(751, 350);
            this.sideNavPanel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 295);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(745, 50);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // productInfo
            // 
            this.productInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productInfo.BackColor = System.Drawing.Color.White;
            this.productInfo.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.productInfo.ForeColor = System.Drawing.Color.Black;
            this.productInfo.Location = new System.Drawing.Point(0, 28);
            this.productInfo.Name = "productInfo";
            // 
            // 
            // 
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn20);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn11);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn12);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn15);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn16);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn17);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn18);
            this.productInfo.PrimaryGrid.Columns.Add(this.gridColumn19);
            this.productInfo.PrimaryGrid.DataSource = this.productInfoVoListBindingSource;
            this.productInfo.Size = new System.Drawing.Size(751, 261);
            this.productInfo.TabIndex = 0;
            this.productInfo.Text = "superGridControl1";
            // 
            // gridColumn20
            // 
            this.gridColumn20.DataPropertyName = "QueueDate";
            this.gridColumn20.EditorType = typeof(SellerCenterLazada.Controls.MyDatePicker);
            this.gridColumn20.Name = "Hẹn giờ";
            this.gridColumn20.Width = 150;
            // 
            // gridColumn11
            // 
            this.gridColumn11.DataPropertyName = "title";
            this.gridColumn11.Name = "Tiêu đề";
            this.gridColumn11.ReadOnly = true;
            // 
            // gridColumn6
            // 
            this.gridColumn6.DataPropertyName = "imageUrlString";
            this.gridColumn6.EditorType = null;
            this.gridColumn6.Name = "Hình ảnh";
            this.gridColumn6.ReadOnly = true;
            this.gridColumn6.RenderType = typeof(SellerCenterLazada.Controls.GridImageEditControl2);
            this.gridColumn6.Visible = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.DataPropertyName = "priceFormatted";
            this.gridColumn7.Name = "Giá";
            this.gridColumn7.ReadOnly = true;
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "discountPriceFormatted";
            this.gridColumn8.Name = "Giá giảm";
            this.gridColumn8.ReadOnly = true;
            // 
            // gridColumn9
            // 
            this.gridColumn9.DataPropertyName = "skuId";
            this.gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleIntInputEditControl);
            this.gridColumn9.Name = "Sku Id";
            this.gridColumn9.ReadOnly = true;
            // 
            // gridColumn10
            // 
            this.gridColumn10.DataPropertyName = "itemId";
            this.gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleIntInputEditControl);
            this.gridColumn10.Name = "Item Id";
            this.gridColumn10.ReadOnly = true;
            // 
            // gridColumn12
            // 
            this.gridColumn12.DataPropertyName = "feedStatus";
            this.gridColumn12.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleIntInputEditControl);
            this.gridColumn12.Name = "Trạng thái";
            this.gridColumn12.ReadOnly = true;
            // 
            // gridColumn15
            // 
            this.gridColumn15.DataPropertyName = "rating";
            this.gridColumn15.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleIntInputEditControl);
            this.gridColumn15.Name = "Xếp hạng";
            this.gridColumn15.ReadOnly = true;
            // 
            // gridColumn16
            // 
            this.gridColumn16.DataPropertyName = "reviews";
            this.gridColumn16.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleIntInputEditControl);
            this.gridColumn16.Name = "Đánh giá";
            this.gridColumn16.ReadOnly = true;
            // 
            // gridColumn17
            // 
            this.gridColumn17.DataPropertyName = "createdTimestamp";
            this.gridColumn17.Name = "Ngày tạo";
            this.gridColumn17.ReadOnly = true;
            // 
            // gridColumn18
            // 
            this.gridColumn18.DataPropertyName = "sellerSku";
            this.gridColumn18.Name = "Seller Sku";
            this.gridColumn18.ReadOnly = true;
            // 
            // gridColumn19
            // 
            this.gridColumn19.DataPropertyName = "stock";
            this.gridColumn19.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleIntInputEditControl);
            this.gridColumn19.Name = "Stock";
            this.gridColumn19.ReadOnly = true;
            // 
            // productInfoVoListBindingSource
            // 
            this.productInfoVoListBindingSource.DataSource = typeof(SellerCenterLazada.Models.ProductInfoVoList);
            // 
            // sideNavPanel2
            // 
            this.sideNavPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sideNavPanel2.Location = new System.Drawing.Point(142, 31);
            this.sideNavPanel2.Name = "sideNavPanel2";
            this.sideNavPanel2.Size = new System.Drawing.Size(751, 289);
            this.sideNavPanel2.TabIndex = 6;
            this.sideNavPanel2.Visible = false;
            // 
            // sideNavItem1
            // 
            this.sideNavItem1.IsSystemMenu = true;
            this.sideNavItem1.Name = "sideNavItem1";
            this.sideNavItem1.Symbol = "";
            this.sideNavItem1.Text = "Menu";
            // 
            // separator1
            // 
            this.separator1.FixedSize = new System.Drawing.Size(3, 1);
            this.separator1.Name = "separator1";
            this.separator1.Padding.Bottom = 2;
            this.separator1.Padding.Left = 6;
            this.separator1.Padding.Right = 6;
            this.separator1.Padding.Top = 2;
            this.separator1.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            // 
            // sideNavItem2
            // 
            this.sideNavItem2.Checked = true;
            this.sideNavItem2.Name = "sideNavItem2";
            this.sideNavItem2.Panel = this.sideNavPanel1;
            this.sideNavItem2.Symbol = "";
            this.sideNavItem2.Text = "Quản lý đăng dạo";
            // 
            // separator2
            // 
            this.separator2.FixedSize = new System.Drawing.Size(3, 1);
            this.separator2.Name = "separator2";
            this.separator2.Padding.Bottom = 2;
            this.separator2.Padding.Left = 6;
            this.separator2.Padding.Right = 6;
            this.separator2.Padding.Top = 2;
            this.separator2.SeparatorOrientation = DevComponents.DotNetBar.eDesignMarkerOrientation.Vertical;
            // 
            // sideNavItem3
            // 
            this.sideNavItem3.Name = "sideNavItem3";
            this.sideNavItem3.Panel = this.sideNavPanel2;
            this.sideNavItem3.Symbol = "";
            this.sideNavItem3.Text = "Phân tích bán hàng";
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Seller lazada";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(896, 384);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 5;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "Seller Other";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 675);
            this.Controls.Add(this.tabManager);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.gridAccount);
            this.Controls.Add(this.ribbonControl1);
            this.DoubleBuffered = false;
            this.Name = "Form2";
            this.Text = "Seller Tool";
            ((System.ComponentModel.ISupportInitialize)(this.userLoginBindingSource)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabManager)).EndInit();
            this.tabManager.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.sideNav1.ResumeLayout(false);
            this.sideNav1.PerformLayout();
            this.sideNavPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.productInfoVoListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private DevComponents.DotNetBar.StyleManager styleManager2;
        private System.Windows.Forms.BindingSource userLoginBindingSource;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl gridAccount;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnDocFile;
        private DevComponents.DotNetBar.ButtonX btnDangNhap;
        private DevComponents.DotNetBar.ButtonX btnThem;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.TabControl tabManager;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.Controls.SideNav sideNav1;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel1;
        private DevComponents.DotNetBar.Controls.SideNavPanel sideNavPanel2;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem1;
        private DevComponents.DotNetBar.Separator separator1;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem2;
        private DevComponents.DotNetBar.Separator separator2;
        private DevComponents.DotNetBar.Controls.SideNavItem sideNavItem3;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl productInfo;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19;
        private System.Windows.Forms.BindingSource productInfoVoListBindingSource;
        private DevComponents.DotNetBar.ButtonX btnHenGio;
        private DevComponents.DotNetBar.ButtonX btnDangDao;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}