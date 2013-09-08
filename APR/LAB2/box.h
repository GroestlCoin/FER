/* 
Deklaracija razreda HookJeeves
Razred implementira Boxov algoritam za traženje minimuma funkcije
Dino Santl
*/

#include "matrix.cc"

class Box
{
  private:
    int pointN;                                                   //broj tocaka
    int dimension;                                            //dimenzija domene
    Matrix *Xc, *Xr;                             //centroid i reflektirana tocka
    double alfa;                                       //kojeficjent refleksije
    double E;                                                      //preciznost
    std::vector<Matrix> pointSet;      //skup tocaka koje predstavljaju simpleks
    std::vector<std::pair<double, double> > eksplicit;    //popis eksplicitnih
    std::vector<void*> implicit;                //popis implicitnih ogranicenja 
    void pointGenerator();                           //biraju se slucajne tocke
    bool functionFlag;                   //oznaka da li je funkcija postavljena
    double (*pntFunction)(Matrix);                      //pokazivac na funkicju
    void memoryFree();                                   //oslobadanje memorije
    void memoryAlloc(int);                              //rezerviranje memorije
    double computeF(const Matrix&);              //funkcija omotac za funkciju
    void setDefault(int);                          //podrazumjevane vrijednosti
    bool checkImplicit(Matrix);                          //provjera implicitnih
    Matrix computeCentroid();            //na temelju pointSet-a racuna centroid
    int worstPoint();                                   //pronadji najgoru tocku
  public:
    Box(const char*);             //konstruktor za direktno citanje iz datoteke
    Box(int N);                    //konstruktor s podrazumjevanim vrijednostima
    Box(int N, double f(Matrix));                                 //konstruktor
    Box(int N, double f(Matrix), const char*);                  //konstruktor
    ~Box();                                                         //destruktor
    void addConstraints(std::vector<std::
    pair<double, double> >, 
                         std::vector<void*>);   //popis implicitnih ogranicenja 
    void loadFromFile(const char*, bool free = true);   //ucitavanje datoteke
    void setFunction(double f(Matrix));                      //postavi funkciju
    std::pair<Matrix, double> findMinimum(); //glavna metoda: trazenje minimuma  
};
