using OpenCvSharp;

namespace remove_shadow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            var name = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            using var m = new Mat(fileName, ImreadModes.Unchanged);
            using var transformed = ShadowTools.RemoveShadow(m);
            //Cv2.ImShow("original", m);
            //Cv2.ImShow("no shadow", transformed);
            var newFileName = $"{name}_new{ext}";
            transformed.SaveImage(newFileName);
            Cv2.WaitKey();
        }
    }
}