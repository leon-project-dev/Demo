using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActionWpf{
    public static class ImagePaths
    {
        const string folderSvg = "pack://application:,,,/ActionWpf;component/Resources/Images/Svg/";        
        const string ImageFloder = "pack://application:,,,/ActionWpf;component/Resources/Images/";

        //
        
        public const string Icon = ImageFloder + "icon.jpg";
        public const string Persion = folderSvg + "persion.svg";
        public const string Project = folderSvg + "project.svg";
        public const string Reduce = folderSvg + "reduce.svg";
        public const string Book = folderSvg + "book.svg";
    }

    public static class Images
    {
        
        public static ImageSource Icon { get { return GetImage(ImagePaths.Icon); } }
        public static ImageSource Persion { get { return GetSvgImage(ImagePaths.Persion); } }
        public static ImageSource Project { get { return GetSvgImage(ImagePaths.Project); } }
        public static ImageSource Reduce { get { return GetSvgImage(ImagePaths.Reduce); } }
        public static ImageSource Book { get { return GetSvgImage(ImagePaths.Book); } }
        static ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        static ImageSource GetSvgImage(string path)
        {
            var svgImageSource = new SvgImageSourceExtension() { Uri = new Uri(path) }.ProvideValue(null);
            return (ImageSource)svgImageSource;
        }

    }
 
}
