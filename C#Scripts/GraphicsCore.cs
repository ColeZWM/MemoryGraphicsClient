using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using ScreenShotImport;
using lense;
using MemoryGraphicsClient.C_Scripts;
using System.Xml.Serialization;
using System.Reflection.Context;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Json.More;

//Note: This client cannot be ran while a software that uses your screen data is open (ex: screenshare)

public class DirectBitmap : IDisposable
{
    public Bitmap Bitmap { get; private set; }
    public Int32[] Bits { get; private set; }
    public bool Disposed { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }

    protected GCHandle BitsHandle { get; private set; }

    public DirectBitmap(int width, int height)
    {
        Width = width;
        Height = height;
        Bits = new Int32[width * height];
        BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
        Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
    }

    public void SetPixel(int x, int y, Color colour)
    {
        int index = x + (y * Width);
        int col = colour.ToArgb();

        Bits[index] = col;
    }

    public Bitmap getBPFromPath(string path)
    {
        Bitmap bitmap = new Bitmap(path);
        return bitmap;
    }
    public Color GetPixel(int x, int y)
    {
        int index = x + (y * Width);
        int col = Bits[index];
        Color result = Color.FromArgb(col);

        return result;
    }

    public void Dispose()
    {
        if (Disposed) return;
        Disposed = true;
        Bitmap.Dispose();
        BitsHandle.Free();
    }
}

namespace MemoryGraphicsClient
{
    public class GraphicsCore
    {
        public static UI ui = new UI();
        public static DirectoryInfo screenShotStorageDi = new DirectoryInfo("D:\\VSPrograms\\MemoryGraphicsClient\\ScreenShotStorage\\");

        public static int screenHeight = 1080;
        public static int screenWidth = 1920;

        public static string screenShotLocation;
        public static int pngCounter = 0;
        public static int[] maxPngCounter = new int[4];

        public static DirectBitmap screen;

        public static int[] similarPixelsIDset;
        public static double[] coreIDSet;
        public static Color[] getColorsInScreen()
        {
            List<Color> screenDataC = new List<Color>();

            Bitmap scr = new Bitmap(screenShotLocation);

            int xa = 0;
            int ya = 0;

            for (int i = 0; i < (screenWidth*screenHeight); i++)
            {
                if ((ya + 1) < screenHeight)
                {
                    if ((xa + 1) < screenWidth)
                    {
                        xa++;
                    }
                    else
                    {
                        xa = 0; ya++;
                    }
                }
                screenDataC.Add(scr.GetPixel(xa, ya));
            }
            return screenDataC.ToArray();
        }

        public static Color[] colorsInScreen;
        public static void emptyScreenshotStorage()
        {
            //wipes all files in ScreenShotStorage
            int imagesTried = 0;
            int[] pngCountersActive = maxPngCounter;
            while (imagesTried < pngCountersActive.Length)
            {
                try
                {
                    File.Delete(screenShotStorageDi + "screen" + pngCountersActive[imagesTried] + ".png");
                }
                catch
                {

                }
                imagesTried++;
            }
            GC.Collect();
        }
        public static void screenshot()
        {
            //takes a screenshot and assigns it with an id, or unique name 
            if ((pngCounter + 1) < maxPngCounter.Length)
            {
                pngCounter++;
                maxPngCounter[pngCounter] = pngCounter;
            }
            else
            {
                emptyScreenshotStorage();
                pngCounter = 0;
                maxPngCounter[pngCounter] = pngCounter;
            }

            screenShotLocation = screenShotStorageDi + "screen" + pngCounter + ".png";
            PrintScreen ps = new PrintScreen();
            ps.CaptureScreenToFile(screenShotLocation, ImageFormat.Png);
            screen = dataManage.convertBitmapToDirect(new Bitmap(screenShotLocation));
            colorsInScreen = getColorsInScreen();
        }

        public static int lookForObjects(DirectBitmap currentBitmap, List<Color> dominateC)
        {
            int bpWidth = currentBitmap.Width;
            int bpHeight = currentBitmap.Height;
            int bpArea = (bpWidth * bpHeight);

            Color currentC;

            int simPixels = 0;

            int xC = 0;
            int yC = 0;

            bool checkIfEqual(Color c) //predicate
            {
                if (c == currentC) { return true; }
                else { return false; }
            }

            List<Color> foundColors = new List<Color>();

            for (int j = 0; j < bpArea; j++)
            {
                currentC = currentBitmap.GetPixel(xC, yC);
                foundColors = dominateC.FindAll(checkIfEqual);
                simPixels += foundColors.Count;
                //Console.WriteLine(currentC);
                //foundColors.Clear();

                if ((yC + 1) < bpHeight)
                {
                    if ((xC + 1) < bpWidth) { xC++; }
                    else { yC++; xC = 0; }
                }
            }

            return simPixels;

        }

        public static int[] getKeyPosition(Color[] keyColors)
        {
            int position = 0;
            List<int> keyPositions = new List<int>();

            for (int i = 0; i < colorsInScreen.Length; i++)
            {
                if (keyColors.Contains(colorsInScreen[i]) & i >= (position+100))
                {
                    position = i;
                    keyPositions.Add(position);
                }
            }
            if(keyPositions.Count == 0) { keyPositions.Add(0); }
            return keyPositions.ToArray();
        }
        public static string scanForObjects(string[] folderBaseObjects, bool writeOutData, out Point pointFoudAt) //used for scanning objects on screen
        {
            screenshot();
            //coreIDSet = dataManage.getCoreIDSets(out similarPixelsIDset);

            string checkAmountForSymbol(double coreID, int simPixels, double[] coreIDSets, int[] simPixSets, string[] objectSets)
            {
                //compares all values given with the set defining values of previously defined objects in order to return if the screen has any objects that have been previously defined
                string output = "0";
                int c = 0;
                for(int i = 0; c < objectSets.Length && output == "0"; i++)
                {
                    if(coreID == coreIDSets[c] && simPixels == simPixSets[c]) { output = objectSets[c]; } 
                    c++;
                }
                return output;
            }

            string quickScan(DirectBitmap screenC, out Point foundAt) //sends out the largest sim pix value detected 
            { 
                if (writeOutData)
                {
                    Console.WriteLine("quickScan Started...");
                }
                string charsOnScreen = " ";

                int screenW = screenC.Width;

                colorsInScreen = getColorsInScreen();

                    int positionFound;
                    string symmbol;

                int bpHeight = 100;
                int bpWidth = 100;

                double coreID = 0;

                int collum = 0;
                int row = 0;

                    DirectBitmap tempBP;

                    int[] keyPositions = getKeyPosition(dataManage.dominateColors);
                    int simPix;

                    foundAt = new Point();
                    for (int i = 0; i < keyPositions.Length; i++)
                    {
                        row = Math.DivRem(keyPositions[i], screenW, out collum);
                        tempBP = lenseData.buildLense(bpWidth, bpHeight, Math.Abs(row-40), Math.Abs(collum - 42), screen);
                        simPix = lookForObjects(tempBP, dataManage.dominateColors.ToList());
                        coreID = dataManage.defineImage(tempBP, simPix, dataManage.coreIDSForChromeData);
                        
                        if (simPix != 0 && writeOutData)
                        {
                            Console.WriteLine("simPixelsFound: " + simPix);
                            foundAt.X = collum;
                            foundAt.Y = row;
                            Console.WriteLine("coreID: " + coreID);
                            Console.WriteLine("row: " + row);
                        }
                        
                        symmbol = checkAmountForSymbol(coreID, simPix, dataManage.coreIDSForChromeData, dataManage.simPixelsForChromeData, dataManage.outputForChromeData);
                        if(symmbol != "0")
                        {
                           charsOnScreen += symmbol;
                        }
                    }

                return charsOnScreen;
            }
            return quickScan(screen, out pointFoudAt);
        }
        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;

                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);


            public static void Main(string[] args)
            {
                GC.Collect();
                Console.Clear();
                Point detectedAt;
                string scanResult = scanForObjects(dataManage.chromeData, true, out detectedAt);
                Console.WriteLine(detectedAt.X + " : " + detectedAt.Y);
                ui.SetCursorPosition(detectedAt.X, detectedAt.Y);   
                Console.WriteLine("symbolsFound: " + scanResult);
            }
        }
    }

    public class cubeDimensions
    {
        private double width; 
        public double Width   
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private double height;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        private double depth;
        public double Depth
        {
            get
            {
                return depth;
            }
            set
            {
                depth = value;
            }
        }
    }

    public class TDpoint
    {
        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        private double z;
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
    }

    public class cube
    {
        private double side1;
        public double Side1
        {
            get
            {
                return side1;
            }
            set
            {
                side1 = value;
            }
        }

        private double side2;
        public double Side2
        {
            get
            {
                return side2;
            }
            set
            {
                side2 = value;
            }
        }

        private double side3;
        public double Side3
        {
            get
            {
                return side3;
            }
            set
            {
                side3 = value;
            }
        }

        private double side4;
        public double Side4
        {
            get
            {
                return side4;
            }
            set
            {
                side4 = value;
            }
        }

        private double side5;
        public double Side5
        {
            get
            {
                return side5;
            }
            set
            {
                side5 = value;
            }
        }

        private double side6;
        public double Side6
        {
            get
            {
                return side6;
            }
            set
            {
                side6 = value;
            }
        }

        private double side7;
        public double Side7
        {
            get
            {
                return side7;
            }
            set
            {
                side7 = value;
            }
        }

        private double side8;
        public double Side8
        {
            get
            {
                return side8;
            }
            set
            {
                side8 = value;
            }
        }

        private double side9;
        public double Side9
        {
            get
            {
                return side9;
            }
            set
            {
                side9 = value;
            }
        }

        private double side10;
        public double Side10
        {
            get
            {
                return side10;
            }
            set
            {
                side10 = value;
            }
        }

        private double side11;
        public double Side11
        {
            get
            {
                return side11;
            }
            set
            {
                side11 = value;
            }
        }

        private double side12;
        public double Side12
        {
            get
            {
                return side12;
            }
            set
            {
                side12 = value;
            }
        }
    }

    public class Pixel
    {
        private double x;

        public double X
        {
            get
            {
                return x;
            }
            set { x = value; }
        }

        private double y;

        public double Y
        {
            get
            {
                return y;
            }
            set { y = value; }
        }

        private Color pixelColor;
        
        public Color PixelColor
        {
            get
            {
                return pixelColor;
            }
            set { pixelColor = value; }
        }
    }
    public class PolygonalRendering
    {
        public static string pathOfLenseImage = "D:\\VSPrograms\\MemoryGraphicsClient\\lenseOutput\\lense1.bmp";
        private static DirectBitmap lenseOfObject;

        private static int lenseWidth;
        private static int lenseHeight;

        private static double[] dimensions;

        private static TDpoint pointOfAttraction;

        private static TDpoint[] constructionPoints;
        public void setLense(DirectBitmap lense)
        {
           lenseOfObject = lense;
           lenseWidth = lenseOfObject.Width;
           lenseHeight = lenseOfObject.Height;

            dimensions = getDimensionsOfObject(lense);
            pointOfAttraction = getPointOfAttraction();
            constructionPoints = getConstructionPointsInCube(getDimensionsOfObject(lense));
        }
        public DirectBitmap getLense() { return lenseOfObject; }

        public TDpoint sendOutPointOfAttraction() {  return pointOfAttraction; }

        public TDpoint[] sendOutConstructionPoints() {  return constructionPoints; }

        public static List<Pixel> convertDirectBitmapToPixelArray(DirectBitmap l, out List<Pixel> yPixelMap)
        {
            List<Pixel> xAxisPixelMap = new List<Pixel>();
            List<Pixel> yAxisPixelMap = new List<Pixel>();
            int xi = 0; int yi = 0;
            int xs = 0; int ys = 0;
            int a = l.Width*l.Height;
            void advance(bool writeOutErrors)
            {
                //advances both x and y axis related x y positions by a value of one
                if((yi+1) < l.Height) 
                {
                    if((xi+1) < l.Width)
                    {
                        xi++;
                    }
                    else
                    {
                        xi = 0; yi++;
                    }
                }
                else
                {
                    if(writeOutErrors) { Console.WriteLine("outOfBounds"); }
                }
                if((xs+1) < l.Width)
                {
                    if((ys+1) < l.Height)
                    {
                        ys++;
                    }
                    else
                    {
                        ys = 0; xs++;
                    }
                }
                else
                {
                    if (writeOutErrors) { Console.WriteLine("outOfBounds"); }
                }
            }
            for(int i = 0; i < a; i++)
            {
                Pixel pixelC = new Pixel();
                advance(false);
                pixelC.X = xi; pixelC.Y = yi;
                pixelC.PixelColor = l.GetPixel((int)pixelC.X, (int)pixelC.Y);
                xAxisPixelMap.Add(pixelC);

                Pixel pixelA = new Pixel();
                pixelA.X = xs; pixelA.Y = ys;
                pixelA.PixelColor = l.GetPixel((int)pixelA.X, (int)pixelA.Y);
                yAxisPixelMap.Add(pixelA);
            }
            yPixelMap = yAxisPixelMap;
            return xAxisPixelMap;
        }

        private static Color[] dColors = dataManage.dominateColors;
        public static Color[] convertPixelsToColorArray(Pixel[] pixels)
        {
            Color[] colorsOut = new Color[pixels.Length];
            for(int i  = 0; i < pixels.Length; i++) 
            {
                colorsOut[i] = pixels[i].PixelColor;
            }
            return colorsOut;
        }
        public static double sq(double x)
        {
            return x * x;
        }
        public static double constructLine(double x2, double x1, double y2, double y1) //gets distance between two given points
        {
            return Math.Sqrt(sq(x2 - x1) + sq(y2 - y1));
        }
        public static double[] getDimensionsOfObject(DirectBitmap l)
        {
            double yMin = 0;
            double yMax = 0;
            double xMin = 0;
            double xMax = 0;

            int a = l.Width*l.Height;
            List<Pixel> yAxisPixelMap;
            List<Pixel> xAxisPixelMap = convertDirectBitmapToPixelArray(lenseOfObject, out yAxisPixelMap);
            int k = a-1;
            for (int i = 0; i < a; i++, k--)
            {
                if (dColors.Contains(xAxisPixelMap[i].PixelColor) && xAxisPixelMap[i].PixelColor != Color.FromArgb(255, 255, 255, 255))
                {
                    yMin = xAxisPixelMap[i].Y;
                    //Console.WriteLine(yMin);
                }
                if (dColors.Contains(xAxisPixelMap[k].PixelColor) && xAxisPixelMap[k].PixelColor != Color.FromArgb(255, 255, 255, 255))
                {
                    yMax = xAxisPixelMap[k].Y;
                    //Console.WriteLine(yMax);
                }
                if (dColors.Contains(xAxisPixelMap[i].PixelColor) && xAxisPixelMap[i].PixelColor != Color.FromArgb(255, 255, 255, 255))
                {
                    xMin = xAxisPixelMap[i].X;
                    //Console.WriteLine(xMin);
                }
                if (dColors.Contains(xAxisPixelMap[k].PixelColor) && xAxisPixelMap[k].PixelColor != Color.FromArgb(255, 255, 255, 255))
                {
                    xMax = xAxisPixelMap[k].X;
                    //Console.WriteLine(xMax);    
                }
            }
            //Console.WriteLine(xMax + " " + xMin);
            //Console.WriteLine(yMax + " " + yMin);

            double width = Math.Abs(xMax - xMin);
            double height = Math.Abs(yMax - yMin);

            double depth;
            Pixel e = new Pixel(); 
            e = xAxisPixelMap[(int)(xMax * yMax)];
            Pixel p = new Pixel();
            p = xAxisPixelMap[(int)(xMax * yMin)];

            double z = constructLine(p.X, e.X, p.Y, e.Y);
            depth = Math.Abs(Math.Sqrt(sq(z) - sq(height)));
            //Console.WriteLine("Dimensions Initial: " + width + " " + height + " " + depth);
            return new double[]
            {
                width, height, depth
            };
        }
        public static TDpoint getPointOfAttraction()
        {
            //gets the point that all vertexes of the cube will begin to transalate towards
            List<Pixel> yAxisPixelMap;
            List<Pixel> xAxisPixelMap = convertDirectBitmapToPixelArray(lenseOfObject, out yAxisPixelMap);
            int indexLocation = xAxisPixelMap.Count/2;
            int x = (int)xAxisPixelMap[indexLocation].X;
            int y = (int)xAxisPixelMap[indexLocation].Y; ;
            TDpoint point = new TDpoint();
            point.X = x;
            point.Y = y;
            point.Z = dimensions[2];
            return point;   
        }

        public static TDpoint[] getConstructionPointsInCube(double[] dim)
        {
            TDpoint[] points = new TDpoint[8];

            double w = dim[0];
            double h = dim[1];
            double d = dim[2];

            //Console.WriteLine(w + " " + h + " " + d);

            TDpoint constructNewPoint (double x, double y, double z) 
            {
                TDpoint newPoint = new TDpoint();
                newPoint.X = x;
                newPoint.Y = y;
                newPoint.Z = z;
                return newPoint;
            }

            points[0] = constructNewPoint(w, h, d); // right, bottom, back
            points[1] = constructNewPoint(0, h, d); // left, bottom, back
            points[2] = constructNewPoint(w, 0, d); // right, top, back
            points[3] = constructNewPoint(0, 0, d); // left, top, back

            points[4] = constructNewPoint(w, h, 0); // right, bottom, front
            points[5] = constructNewPoint(0, h, 0); // left, bottom, front
            points[6] = constructNewPoint(w, 0, 0); // right, top, front
            points[7] = constructNewPoint(0, 0, 0); // left, top, front

            return points;
        }
        public static cube constructCube(TDpoint[] points)
        {
            //gets the length of eevery side in the cube
            cube c = new cube();
            c.Side1 = constructLine(points[1].X, points[0].X, points[1].Y, points[0].Y);
            c.Side2 = constructLine(points[2].X, points[0].X, points[2].Y, points[0].Y);
            c.Side3 = constructLine(points[3].X, points[1].X, points[3].Y, points[1].Y);
            c.Side4 = constructLine(points[2].X, points[3].X, points[2].Y, points[3].Y);
            c.Side5 = constructLine(points[3].X, points[7].X, points[3].Y, points[7].Y);
            c.Side6 = constructLine(points[2].X, points[6].X, points[2].Y, points[6].Y);
            c.Side7 = constructLine(points[0].X, points[4].X, points[0].Y, points[4].Y);
            c.Side8 = constructLine(points[1].X, points[5].X, points[1].Y, points[5].Y);
            c.Side9 = constructLine(points[5].X, points[4].X, points[5].Y, points[4].Y);
            c.Side10 = constructLine(points[6].X, points[4].X, points[6].Y, points[4].Y);
            c.Side11 = constructLine(points[7].X, points[5].X, points[7].Y, points[5].Y);
            c.Side12 = constructLine(points[6].X, points[7].X, points[6].Y, points[7].Y);
            return c;
        }
        public cube shrinkWrapCube(double shrinkFactor, out TDpoint[] pointsPostShrink)
        {
            cube cubePostTransalate;
            TDpoint[] cPoints = constructionPoints;
            List<TDpoint> pointsPShrink = new List<TDpoint>();
            TDpoint c = new TDpoint();
            int rawPointLocation;

            List<Pixel> yAxisPixelMap;
            List<Pixel> xAxisPixelMap = convertDirectBitmapToPixelArray(lenseOfObject, out yAxisPixelMap);
            for (int j = 0; j < cPoints.Length; j++)
                {
                    c = cPoints[j];
                    rawPointLocation = (int)(c.X * c.Y);
                for (int i = 0; i < shrinkFactor; i++)
                {
                    if (!dColors.Contains(xAxisPixelMap[rawPointLocation].PixelColor))
                    {
                        //Console.WriteLine(c.X + ":" + cPoints[j].Y);
                        if (c.X > PolygonalRendering.pointOfAttraction.X && c.Y > PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x--, y--
                            c.X--;
                            c.X = Math.Abs(c.X);    
                            c.Y--;
                            c.Y = Math.Abs(c.Y);
                        }
                        if (c.X < PolygonalRendering.pointOfAttraction.X && c.Y < PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x++, y++
                            c.X++;
                            c.Y++;
                        }
                        if (c.X > PolygonalRendering.pointOfAttraction.X && c.Y < PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x--, y++
                            c.X--;
                            c.X = Math.Abs(c.X);
                            c.Y++;
                        }
                        if (c.X < PolygonalRendering.pointOfAttraction.X && c.Y > PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x++, y--
                            c.X++;
                            c.Y--;
                            c.Y = Math.Abs(c.Y);
                        }
                        if (c.X > PolygonalRendering.pointOfAttraction.X && c.Y < PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x--, y++
                            c.X--;
                            c.X = Math.Abs(c.X);
                            c.Y++;
                        }
                        if (c.X > PolygonalRendering.pointOfAttraction.X && c.Y == PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x--
                            c.X--;
                            c.X = Math.Abs(c.X);
                        }
                        if (c.X < PolygonalRendering.pointOfAttraction.X && c.Y == PolygonalRendering.pointOfAttraction.Y)
                        {
                            //x++
                            c.X++;
                        }
                        if (c.X == PolygonalRendering.pointOfAttraction.X && c.Y > PolygonalRendering.pointOfAttraction.Y)
                        {
                            //y--
                            c.Y--;
                            c.Y = Math.Abs(c.Y);
                        }
                        if (c.X == PolygonalRendering.pointOfAttraction.X && c.Y < PolygonalRendering.pointOfAttraction.Y)
                        {
                            //y++
                            c.Y++;
                        }
                    }
                }
                pointsPShrink.Add(c);
            }
            pointsPostShrink = pointsPShrink.ToArray();
            cubePostTransalate = constructCube(cPoints);
            //for (int i = 0; i < pointsPostShrink.Length; i++)
            //{
                //Console.WriteLine(pointsPostShrink[i].X + "" + pointsPostShrink[i].Y);
            //}
            return cubePostTransalate;
        }
    }
}

namespace lense
{
    public static class lenseData
    {
        public static DirectBitmap buildLense(int objW, int objH, int row, int collum, DirectBitmap screen)
        {
            DirectBitmap lenseOut = new DirectBitmap(objW, objH);

            int x = 0;
            int y = 0;

            int objectArea = objW * objH;
            
            lenseOut.SetPixel(x, y, screen.GetPixel(x + collum, y + row));

            for (int i = 0; i < objectArea; i++)
            {   
                //Console.WriteLine(x + ":" + y);

                    if ((x+1) < objW) { x++; }
                    else if ((y+1) < objH) { x = 0; y++; }
                    else { }

                if ((x+collum) < screen.Width && (y+row) < screen.Height)
                {
                    lenseOut.SetPixel(x, y, screen.GetPixel(x + collum, y + row));
                }
            }
            //Console.WriteLine("lenseBuildCalled");
            if (!File.Exists("D:\\VSPrograms\\MemoryGraphicsClient\\lenseOutput\\lense1.bmp"))
            {
                File.Create("D:\\VSPrograms\\MemoryGraphicsClient\\lenseOutput\\lense1.bmp");
            }
            lenseOut.Bitmap.Save("D:\\VSPrograms\\MemoryGraphicsClient\\lenseOutput\\lense1.bmp");
            return lenseOut;
        }
    }
}