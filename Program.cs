using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        static Bresenham meinZeichnungsObjekt = new Bresenham();

        // Punkt Struktur Koordinaten
        public struct Punkt
        {

            public int xpos;
            public int ypos;

            public Punkt(int px, int py)
            {
                xpos = px;
                ypos = py;
            }
        }

        // Dreieck Strukur Eckpunkte
        public struct Dreieck
        {

            public Punkt punkt1;
            public Punkt punkt2;
            public Punkt punkt3;

            public Dreieck(Punkt p1, Punkt p2, Punkt p3)
            {
                punkt1 = p1;
                punkt2 = p2;
                punkt3 = p3;
            }
        };


        public static void ZeichneDreieck(Dreieck dr)
        {

            meinZeichnungsObjekt.Zeichne(dr.punkt1, dr.punkt2);
            meinZeichnungsObjekt.Zeichne(dr.punkt2, dr.punkt3);
            meinZeichnungsObjekt.Zeichne(dr.punkt3, dr.punkt1);

        }

        static void Main(string[] args)
        {
            List<Dreieck> meineDreiecke = new List<Dreieck>();

            meineDreiecke.Add(new Dreieck(new Punkt(15, 10), new Punkt(3, 4), new Punkt(8, 12)));
            meineDreiecke.Add(new Dreieck(new Punkt(30, 30), new Punkt(50, 30), new Punkt(40, 17)));
            meineDreiecke.Add(new Dreieck(new Punkt(60, 60), new Punkt(100, 60), new Punkt(80, 34)));

            for (int i = 0; i < meineDreiecke.Count;i++)
            {
                ZeichneDreieck(meineDreiecke[i]);
            }

            Console.ReadKey();

        }



        class Bresenham
        {
            private int x, y, t, dx, dy, incx, incy, pdx, pdy, ddx, ddy, deltaslowdirection, deltafastdirection, err;

            private void Init(int xstart, int ystart, int xend, int yend)
            {
                // Entfernung in beiden Dimensionen berechnen
                dx = xend - xstart;
                dy = yend - ystart;

                // Vorzeichen des Inkrements bestimmen
                incx = Math.Sign(dx);
                incy = Math.Sign(dy);
                if (dx < 0) dx = -dx;
                if (dy < 0) dy = -dy;

                // feststellen, welche Entfernung größer ist
                if (dx > dy)
                {
                    // x ist schnelle Richtung 
                    pdx = incx; pdy = 0;    // pd. ist Parallelschritt 
                    ddx = incx; ddy = incy; // dd. ist Diagonalschritt 
                    deltaslowdirection = dy; deltafastdirection = dx;   // Delta in langsamer Richtung, Delta in schneller Richtung 
                }
                else
                {
                    // y ist schnelle Richtung 
                    pdx = 0; pdy = incy; // pd. ist Parallelschritt 
                    ddx = incx; ddy = incy; // dd. ist Diagonalschritt 
                    deltaslowdirection = dx; deltafastdirection = dy;   //( Delta in langsamer Richtung, Delta in schneller Richtung)
                }

                // Initialisierungen vor Schleifenbeginn 
                x = xstart;
                y = ystart;
                err = deltafastdirection / 2;

                t = 0;

                Console.SetCursorPosition(x, y); ;  //Cursor in Konsolenfenster an Linienanfang setzen
                Console.Write("°"); //Dort ein '°' ausgeben
            }

            public void Zeichne(Punkt start, Punkt end)
            {
                Init(start.xpos, start.ypos, end.xpos, end.ypos);

                while (Gbham() == 0)  //Punkt für Punkt solange bis Algorithmus abbricht (Linie zuende ist)
                {
                    //.ReadKey();    //Wenn das Zeichnen jedes '°' bestätigt werden soll
                }
            }

            private int Gbham()
            /*---------------------------------------------------------------------------
            * Bresenham-Algorithmus: Linien auf Rastergeräten zeichnen
            *         
            *  void Zeichne(Punkt start, Punkt end)
            *     Punkt start        = Koordinaten des Startpunkts
            *     Punkt end          = Koordinaten des Endpunkts
            *     
            *     Zeichnet die mit init() angegebene Linie mit '°' auf den Konsolenschirm
            *----------------------------------------------------------------------------*/
            {
                // t zaehlt die Pixel, deltafastdirection ist die Anzahl der Schritte
                t++;

                if (t >= deltafastdirection)
                    return -1;    //Linie zuende => Abbruch des Algorithmus
                                  // Pixel berechnen 
                                  // Aktualisierung Fehlerterm
                err -= deltaslowdirection;
                if (err < 0)
                {
                    // Fehlerterm wieder positiv (>=0) machen 
                    err += deltafastdirection;
                    // Schritt in langsame Richtung, Diagonalschritt 
                    x += ddx;
                    y += ddy;
                }
                else
                {
                    // Schritt in schnelle Richtung, Parallelschritt 
                    x += pdx;
                    y += pdy;
                }
                Console.SetCursorPosition(x, y);
                Console.Write("°");
                return 0;   //Linie noch nicht zuende; Weiterer Aufruf nötig / möglich

            } //Gbham()
        }
    }
}
