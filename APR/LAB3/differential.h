/* 
Deklaracija razreda Trapez i RungeKutta
Razred Diferential koristi se kao apstrakcija za rjesacanje diferencijalnih jednadzbi 
Dino Santl
*/
#ifndef DIFF_CLASS
#define DIFF_CLASS

#include "matrix.h"

class DiffCompute
{
  protected:
    Matrix A; //matrica A
    Matrix B; //matrica B
    Matrix X0; //pocetni uvijeti
    double T; //korak integracije
    std::pair<double, double> interval; //zadani interval
    int userNumOfSteps;
    
    void loadParameters(const char*); //ucitavanje intervala, koraka i pocetnog uvijeta
    
  public:
    //datoteka za matricu A, matricu B, matrica X0, zajednicka za korak integracije i interval
    DiffCompute(int, const char*, const char*, const char*, const char*); //konstruktor
    virtual void Run()=0; //pokretanje metode koja ispisuje rezultate
};


class Trapez : public DiffCompute
{
  private:
    
  public:
    //datoteka za matricu A, matricu B, matrica X0, zajednicka za korak integracije i interval
    Trapez(int, const char*, const char*, const char*, const char*); //konstruktor
    void Run(); //pokretanje metode koja ispisuje rezultate
};

class RungeKutta : public DiffCompute
{
  private:
    Matrix F(const Matrix&); //racunanje derivacije x'=f(x)
  public:
    //datoteka za matricu A, matricu B, matrica X0, zajednicka za korak integracije i interval
    RungeKutta(int, const char*, const char*, const char*, const char*); //konstruktor
    void Run(); //pokretanje metode koja ispisuje rezultate
};

#endif
