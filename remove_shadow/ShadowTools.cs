using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remove_shadow
{
    internal class ShadowTools
    {
        public static Mat RemoveShadow(Mat original)
        {
            var rgb_planes = original.Split();
            //cn == 1错误:dilate的输入mat只能是单通道图片
            using var dilateMat =Mat.Ones(7, 7, MatType.CV_8UC1);
            List<Mat> result_planes = new List<Mat>();
            foreach(var plane in rgb_planes)
            {
                var dilated = plane.Dilate(dilateMat);
                var bg_img = dilated.MedianBlur(21);
                Mat diff = new Mat(plane.Rows, plane.Cols, plane.Type());
                Cv2.Absdiff(plane, bg_img, diff);
                var diff_img = 255 - diff;
                var normalized = diff_img.ToMat().Normalize(0,255,NormTypes.MinMax);
                //var normalized = diff_img.ToMat().Normalize(1,0,NormTypes.L2);
                result_planes.Add(normalized);
                //result_planes.Add(diff_img);
            }
            Mat ret = new Mat(original.Rows, original.Cols, original.Type());
            //Cv2.Merge(result_planes_normalized.ToArray(), ret);
            Cv2.Merge(result_planes.ToArray(), ret);
            return ret;
        }
    }
}
