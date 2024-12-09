using lense;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.ObjectPool;
using System.Drawing;

namespace MemoryGraphicsClient.C_Scripts
{
    public class dataManage
    {

        public static string[] arial_22P_WBlack = new string[] {
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WBlack\\Screenshot 2024-04-19 115216_m.png",
        };

        public static string[] arial_22P_WGreen = new string[]
        {
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135651_A(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135719_a(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135740_B(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135801_b(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135825_C(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135844_c(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135908_D(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 135940_d(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140015_E(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140033_e(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140049_F(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140154_f(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140218_G(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140246_g(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140305_H(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140329_h(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140414_I(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140434_i(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140457_J(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140522_j(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140623_K(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 140644_k(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152414_L(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152433_l(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152449_M(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152504_m(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152521_N(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152539_n(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152557_O(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152618_o(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152635_P(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152654_p(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152714_Q(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152732_q(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152756_R(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152817_r(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152836_S(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152853_s(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152910_T(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 152943_t(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153003_U(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153027_u(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153046_V(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153102_v(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153119_W(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153136_w(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153151_X(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153208_x(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153236_Y(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153255_y(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153417_Z(g).png",
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Arial_22P_WGreen\\Screenshot 2024-07-18 153437_z(g).png"
        };

        public static string[] lettersEnglish = new string[]
        {
            "A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "J", "j", "K", "k", "L", "l",
            "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s", "T", "t", "U", "u", "V", "v", "W", "w", "X", "x",
            "Y", "y", "Z", "z"
        };

        public static double[] coreIDsForLettersEnglish = new double[]
        {
            76.65763620291176,
            73.17355110725893,
            75.52248781407008,
            80.03323369975531,
            82.26361996004553,
            80.91527971260894,
            75.52248781407008,
            88.89809016066295,
            70.91787519639712,
            80.91527971260894,
            88.89809016066295,
            52,
            80.03323369975531,
            83.37419043517701,
            69.74775325781236,
            75.52248781407008,
            88.89809016066295,
            88.89809016066295,
            88.89809016066295,
            66,
            68.5687454961729,
            75.52248781407008,
            74.38150172435145,
            88.89809016066295,
            63.74878606504425,
            52.75221910416593,
            69.74775325781236,
            69.99480991184053,
            82.26361996004553,
            80.91527971260894,
            88.89809016066295,
            88.89809016066295,
            74.46320593184886,
            78.9125107890294,
            66.18115860654379,
            88.49204224576118,
            80.03323369975531,
            80.91527971260894,
            78.9125107890294,
            86.5601872324848,
            75.52248781407008,
            69.99480991184053,
            76.65763620291176,
            77.84680253099214,
            62.513573749618715,
            61.726286368634966,
            67.38013505195957,
            66.75043740941331,
            76.65763620291176,
            85.58827421422983,
            72.07978686060771,
            69.99480991184053
        };

        public static int[] simPixelsForLettersEnglish = new int[]
        {
             298
            ,257
            ,360
            ,262
            ,285
            ,192
            ,337
            ,278
            ,276
            ,248
            ,219
            ,164
            ,341
            ,327
            ,306
            ,236
            ,135
            ,96
            ,187
            ,135
            ,329
            ,229
            ,174
            ,108
            ,479
            ,331
            ,362
            ,208
            ,338
            ,231
            ,281
            ,263
            ,378
            ,280
            ,357
            ,113
            ,321
            ,220
            ,189
            ,150
            ,298
            ,210
            ,265
            ,182
            ,475
            ,325
            ,311
            ,211
            ,229
            ,229
            ,260
            ,188
        };

        public static string[] chromeData = new string[]
        {
            "D:\\VSPrograms\\MemoryGraphicsClient\\ObjectImgs\\Screenshot_chromeLogo.png"
        };

        public static string[] outputForChromeData = new string[]
        {
            "logoFound_Chrome"
        };

        public static double[] coreIDSForChromeData = new double[]
        {
           88.97680669631386
        };

        public static int[] simPixelsForChromeData = new int[]
        {
            449
        };

        public static int getRiseOfSlope(int y1, int y2)
        {
            return y2 - y1;
        }

        public static int getRunOfSlope(int x1, int x2)
        {
            return x2 - x1;
        }

        public static double getSlope(int rise, int run)
        {
            if (rise != 0 && run != 0)
            {
                return rise / run;
            }
            else
            {
                return 0;
            }
        }

        public static int square(int x)
        {
            return x * x;
        }

        public static double squareDouble(double x)
        {
            return x * x;
        }

        public static Color[] getDominateColors(string[] samples, List<Color> blacklist)
        {
            Console.WriteLine("gettingDominateColorsFromSamples...");
            DirectBitmap dBP;
            List<Color> domColors = new List<Color>();

            int c = 0;
            for (int k = 0; k < samples.Length; k++)
            {
                dBP = convertBitmapToDirect(new Bitmap(samples[c]));

                int width = dBP.Width;
                int height = dBP.Height;
                int area = width * height;

                Color currentColor;
                int x = 0, y = 0;

                for (int i = 0; i < area; i++)
                {
                    if ((y + 1) < height)
                    {
                        if ((x + 1) < width)
                        {
                            x++;
                        }
                        else
                        {
                            x = 0;
                            y++;
                        }
                    }

                    currentColor = dBP.GetPixel(x, y);
                    if (!domColors.Contains(currentColor) && !blacklist.Contains(currentColor) && (currentColor.R > 40 && currentColor.G > 40 && currentColor.B > 40))
                    {
                        Console.WriteLine(currentColor);
                        domColors.Add(currentColor);
                    }
                }
                c++;
            }
            Color[] filteredDomColors = domColors.ToArray();
            Console.WriteLine("UniqueColors: " + filteredDomColors.Length);
            return filteredDomColors;
        }
        public static List<Color> blackListDColors = new List<Color>() { Color.FromArgb(255, 37, 36, 31), Color.FromArgb(255, 33, 42, 36), Color.FromArgb(255, 37, 36, 34), Color.FromArgb(255, 35, 34, 34), Color.FromArgb(255, 33, 34, 33), Color.FromArgb(255, 34, 33, 32), Color.FromArgb(255, 34, 40, 36), Color.FromArgb(255, 42, 40, 34), Color.FromArgb(255, 35, 34, 34), Color.FromArgb(255, 36, 33, 33), Color.FromArgb(255, 35, 33, 33), Color.FromArgb(255, 33, 31, 39), Color.FromArgb(255, 31, 31, 31), Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 33, 33, 33), Color.FromArgb(255, 34, 34, 34), Color.FromArgb(255, 35, 35, 35), Color.FromArgb(255, 36, 36, 36), Color.FromArgb(255, 37, 37, 37), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 38, 38, 38), Color.FromArgb(255, 39, 39, 39),Color.FromArgb(255, 255, 255, 255) };
        public static Color[] dominateColors = getDominateColors(chromeData, blackListDColors);
        public static double defineImage(DirectBitmap ImageToDefine, int simPix, double[] coreIDSToCheck)
        {
            int width = ImageToDefine.Width;
            int height = ImageToDefine.Height;
            int area = width * height;

            double getDistanceOfLine(int x2, int x1, int y2, int y1)
            {
                double returnVal;
                returnVal = Math.Sqrt(square(x2 - x1) + square(y2 - y1));
                return returnVal;
            }

            double valOut = 0;
            double angleZ;

            if (simPix != 0)
            {
                PolygonalRendering polyRendering = new PolygonalRendering();
                polyRendering.setLense(ImageToDefine);

                TDpoint[] pointsPShrink;
                cube shrinkCube = polyRendering.shrinkWrapCube(1, out pointsPShrink); //change param as needed
                
                double bL = shrinkCube.Side3;

                double cL = PolygonalRendering.constructLine(pointsPShrink[2].X, pointsPShrink[0].X, pointsPShrink[2].Y, pointsPShrink[0].Y);
                double vL = PolygonalRendering.constructLine(pointsPShrink[2].X, pointsPShrink[3].X, pointsPShrink[2].Y, pointsPShrink[3].Y);

                valOut = double.RadiansToDegrees(Math.Acos((squareDouble(vL) + squareDouble(bL) - squareDouble(cL)) / (2 * vL * bL)));

                if(double.IsNaN(valOut) ) { valOut = vL + bL + cL; }
            }
            return valOut;
        }
        public static DirectBitmap convertBitmapToDirect(Bitmap bp)
        {
            int w = bp.Width;
            int h = bp.Height;
            int a = w * h;
            DirectBitmap dBP = new DirectBitmap(w, h);
            int x = 0;
            int y = 0;
            for (int i = 0; i < a; i++)
            {
                dBP.SetPixel(x, y, bp.GetPixel(x, y));
                if ((y + 1) < h) { if ((x + 1) < w) { x++; } else { x = 0; y++; } }
            }
            return dBP;
        }
    }
}