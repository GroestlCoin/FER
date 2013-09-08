/* 
Deklaracija razreda HookJeeves
Razred implementira Hook Jeeves algoritam za traženje minimuma funkcije
Dino Santl
*/
#include <vector>
#include "matrix.cc"

class HookJeeves
{
  private:
    Matrix *dX, *E, *Xb, *Xp, *Xn;                            //stanje algoritma
    int dimension;                                           //dimenzija domene
    bool functionFlag;                   //oznaka da li je funkcija postavljena
    double (*pntFunction)(Matrix);                      //pokazivac na funkicju
    Matrix explore();                             //funkcija istrazivanja oko Xp
    void memoryFree();                                   //oslobadanje memorije
    void memoryAlloc(int);                              //rezerviranje memorije
    double computeF(const Matrix&);              //funkcija omotac za funkciju
    void setDefault(int);                          //podrazumjevane vrijednosti
  public:
    HookJeeves(const char*);      //konstruktor za direktno citanje iz datoteke
    HookJeeves(int N);            //konstruktor s podrazumjevanim vrijednostima
    HookJeeves(int N, double f(Matrix));                          //konstruktor
    HookJeeves(int N, double f(Matrix), const char*);           //konstruktor
    ~HookJeeves();                //destruktor
    void loadFromFile(const char*, bool free = true);//ucitavanje iz datoteke
    void setFunction(double f(Matrix));                     //postavi funkciju
    std::pair<Matrix, double> findMinimum(); //glavna metoda: trazenje minimuma
};
