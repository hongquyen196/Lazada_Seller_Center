using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace SellerCenterLazada.Controls
{
    [ToolboxItem(false)]
    public class GridImageEditControl2 : Control, IGridCellEditControl
    {
        private EditorPanel _EditorPanel;
        private bool _SuspendUpdate;
        private StretchBehavior _StretchBehavior = StretchBehavior.Both;
        private GridCell _Cell;
        private Bitmap _EditorCellBitmap;
        private Image _Image;
        private ImageSizeMode _ImageSizeMode = ImageSizeMode.Normal;

        private Image EffectiveImage
        {
            get
            {
                return _Image;
            }
        }

        public bool CanInterrupt
        {
            get
            {
                return true;
            }
        }

        public CellEditMode CellEditMode
        {
            get
            {
                return CellEditMode.InPlace;
            }
        }

        public GridCell EditorCell
        {
            get
            {
                return this._Cell;
            }
            set
            {
                this._Cell = value;
            }
        }
        public Bitmap EditorCellBitmap
        {
            get
            {
                return this._EditorCellBitmap;
            }
            set
            {
                if (this._EditorCellBitmap != null)
                {
                    this._EditorCellBitmap.Dispose();
                }
                this._EditorCellBitmap = value;
            }
        }

        public virtual string EditorFormattedValue
        {
            get
            {
                string result;
                if (this._Cell != null && this._Cell.Value != null)
                {
                    result = this._Cell.Value.ToString();
                }
                else
                {
                    result = this.Text;
                }
                return result;
            }
        }

        public EditorPanel EditorPanel
        {
            get
            {
                return this._EditorPanel;
            }
            set
            {
                this._EditorPanel = value;
            }
        }
        public object EditorValue {
            get
            {
                return null;
            }
            set
            {
                var a = value;
            }
        }
        public bool EditorValueChanged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Type EditorValueType => throw new NotImplementedException();

        public virtual StretchBehavior StretchBehavior
        {
            get
            {
                return this._StretchBehavior;
            }
            set
            {
                this._StretchBehavior = value;
            }
        }

        public bool SuspendUpdate
        {
            get
            {
                return this._SuspendUpdate;
            }
            set
            {
                this._SuspendUpdate = value;
            }
        }

        public ValueChangeBehavior ValueChangeBehavior {
            get
            {
                return ValueChangeBehavior.InvalidateRender;
            }
        }

        public bool BeginEdit(bool selectAll)
        {
            return true;
        }

        public bool CancelEdit()
        {
            return true;
        }

        public void CellKeyDown(KeyEventArgs e)
        {
        }

        public void CellRender(Graphics g)
        {
            Image effectiveImage = this.EffectiveImage;
            if (effectiveImage != null)
            {
                Rectangle cellEditBounds = this._Cell.GetCellEditBounds(this);
                switch (this._ImageSizeMode)
                {
                    case ImageSizeMode.Normal:
                        {
                            Rectangle srcRect = cellEditBounds;
                            srcRect.Location = Point.Empty;
                            g.DrawImage(effectiveImage, cellEditBounds, srcRect, GraphicsUnit.Pixel);
                            break;
                        }
                    case ImageSizeMode.StretchImage:
                        g.DrawImage(effectiveImage, cellEditBounds);
                        break;
                    case ImageSizeMode.CenterImage:
                        this.RenderImageCentered(g, effectiveImage, cellEditBounds);
                        break;
                    case ImageSizeMode.Zoom:
                        this.RenderImageScaled(g, effectiveImage, cellEditBounds);
                        break;
                }
            }
        }

        private void RenderImageScaled(Graphics g, Image image, Rectangle r)
        {
            SizeF sizeF = new SizeF((float)image.Width / image.HorizontalResolution, (float)image.Height / image.VerticalResolution);
            float num = Math.Min((float)r.Width / sizeF.Width, (float)r.Height / sizeF.Height);
            sizeF.Width *= num;
            sizeF.Height *= num;
            g.DrawImage(image, (float)r.X + ((float)r.Width - sizeF.Width) / 2f, (float)r.Y + ((float)r.Height - sizeF.Height) / 2f, sizeF.Width, sizeF.Height);
        }

        private void RenderImageCentered(Graphics g, Image image, Rectangle r)
        {
            Rectangle srcRect = r;
            srcRect.Location = Point.Empty;
            if (image.Width > r.Width)
            {
                srcRect.X += (image.Width - r.Width) / 2;
            }
            else
            {
                r.X += (r.Width - image.Width) / 2;
            }
            if (image.Height > r.Height)
            {
                srcRect.Y += (image.Height - r.Height) / 2;
            }
            else
            {
                r.Y += (r.Height - image.Height) / 2;
            }
            g.DrawImage(image, r, srcRect, GraphicsUnit.Pixel);
        }

        public bool EndEdit()
        {
            return true;
        }

        public Size GetProposedSize(Graphics g, GridCell cell, CellVisualStyle style, Size constraintSize)
        {
            return new Size(0, 0);
        }

        public void InitializeContext(GridCell cell, CellVisualStyle style)
        {
            this._Cell = cell;
            if(cell != null && cell.Value != null && (cell.Value.ToString().Contains("http://") || cell.Value.ToString().Contains("https://")))
            {
                var request = WebRequest.Create(cell.Value.ToString());
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var image = Bitmap.FromStream(stream);
                        int n = image.Height / 100;
                        Bitmap resized = new Bitmap(image, new Size(image.Width / n, image.Height / n));
                        this._Image = resized;
                    }
                }
                return;
            }
            if(cell != null && cell.Value != null)
                this._Image = new Bitmap(cell.Value.ToString());
        }

        public void OnCellMouseDown(MouseEventArgs e)
        {
        }

        public void OnCellMouseEnter(EventArgs e)
        {
        }

        public void OnCellMouseLeave(EventArgs e)
        {
        }

        public void OnCellMouseMove(MouseEventArgs e)
        {
        }

        public void OnCellMouseUp(MouseEventArgs e)
        {
        }

        public bool WantsInputKey(Keys key, bool gridWantsKey)
        {
            return true;
        }
    }
}
