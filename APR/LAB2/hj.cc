/*
Implementacija razreda HookJeeves
Predmet: Analiza i projektiranje racunalom
Autor: Dino Šantl
*/
#ifndef __HOOK_JEEVES_MODULE
#define __HOOK_JEEVES_MODULE

#include "hj.h"

/*
method: setFunction
input(double): Broj tipa double
comment: Postavlja se pokazivac na funkciju
*/
void HookJeeves::setFunction(double f(Matrix))
{
  pntFunction = f;
  functionFlag = true;
}

/*
method: memoryFree
comment: Oslobadanje zauzete memorije
*/
void HookJeeves::memoryFree()
{
  delete dX;
  delete E;
  delete Xp;
  delete Xb;
  delete Xn;
}

/*
method: memoryAlloc
input(int): Dimenzija domene funkcije
comment: Rezerviranje memorije
*/
void HookJeeves::memoryAlloc(int N)
{
  dX = new Matrix(N, 1);
  E  = new Matrix(N, 1);
  Xp = new Matrix(N, 1);
  Xb = new Matrix(N, 1);
  Xn = new Matrix(N, 1); 
}

/*
destruktor: ~HookJeeves
comment: Poziva metodu za oslobadanje memorije
*/
HookJeeves::~HookJeeves()
{
  memoryFree();
}

/*
method: loadFromFile
input(const char*, bool): Ime datoteke, da li funkcija treba osloboditi memoriju
comment: U strukturu se spremaju podaci ucitani iz datoteke
*/
void HookJeeves::loadFromFile(const char* fileName, bool free)
{
  int N;
  double tmp;
  FILE *f = fopen(fileName, "r");
  
  fscanf(f, "%d", &N);
   
  dimension = N;
   
  if ( free ) memoryFree();  
  memoryAlloc(N);
  
  for(int i = 0 ; i < N ; ++i)
  {
    fscanf(f, "%lf", &tmp);
    (*dX)[ i ][ 0 ] = tmp;
  }
  
  for(int i = 0 ; i < N ; ++i)
  {
    fscanf(f, "%lf", &tmp);
    (*E)[ i ][ 0 ] = tmp;
  }
  
  for(int i = 0 ; i < N ; ++i)
  {
    fscanf(f, "%lf", &tmp);
    (*Xb)[ i ][ 0 ] = tmp;
    (*Xp)[ i ][ 0 ] = tmp;
  }
 
  fclose(f);
}

/*
constructor: HookJeeves
input(int, (double*)(Matrix)): Dimenzija domene funkcije, pokazivac na funkciju
*/
HookJeeves::HookJeeves(int N, double F(Matrix))
{
  functionFlag = true;
  pntFunction = F;
  dimension = N;
  setDefault( N );  
}

/*
constructor: HookJeeves
input(int, (double*)(Matrix), const char*): Dimenzija domene funkcije
                                            pokazivac na funkciju
                                            ime datoteke
comment: Koriste se parametri iz datoteke
*/
HookJeeves::HookJeeves(int N, double F(Matrix), const char *fileName)
{
  functionFlag = true;
  pntFunction = F;
  dimension = N;
  loadFromFile(fileName, false);
  if ( dimension != N ) throw 2;
}

/*
method: setDefault
input(int): Dimenzija domene funkcije
comment: Koriste se podrazumjevanje vrijednosti
*/
void HookJeeves::setDefault(int N)
{
  memoryAlloc(N);   
  for(int i = 0 ; i < N ; ++i)
  {
    (*dX)[ i ][ 0 ] = 0.5;
    (*E)[ i ][ 0 ] = 1E-9; 
    (*Xb)[ i ][ 0 ] = 0;
    (*Xp)[ i ][ 0 ] = 0;
  }
}

/*
constructor: HookJeeves
input(int): Dimenzija domene funkcije
comment: Koristi podrazumjevanje vrijednosti
*/
HookJeeves::HookJeeves(int N)
{
   functionFlag = false;  
   dimension = N;
   setDefault( N );
}

/*
constructor: HookJeeves
input(const char*): Ime datoteke
comment: Parametri za algoritam se citaju iz datoteke
*/
HookJeeves::HookJeeves(const char *fileName)
{
  functionFlag = false;
  loadFromFile(fileName, false);
}

/*
method: computeF
input(const Matrix&): Vektor koji predstavlja ulazne parametre funkcije
output(double): Vrijednost funkcije za ulazne parametre
*/
double HookJeeves::computeF(const Matrix& M)
{
  if (functionFlag == false) throw 1;
  return (*pntFunction)(M); 
}

/*
method: explore
output(Matrix): Vraca najbolju tocku u istrazivanju
*/
Matrix HookJeeves::explore()
{
  Matrix Xn = *Xp;
  double tmpBest;
  
  for(int i = 0 ; i < dimension ; ++i)
  {
    tmpBest = computeF( Xn );
    Xn[ i ][ 0 ] += (*dX)[ i ][ 0 ];
    if ( computeF( Xn ) > tmpBest )    
      Xn[ i ][ 0 ] -= 2*(*dX)[ i ][ 0 ];
    if ( computeF(Xn) > tmpBest )
      Xn[ i ][ 0 ] += (*dX)[ i ][ 0 ];
  }
  
  return Xn;
}

/*
method: findMinimum
output(pair<Matrix, double>): Vraca par gdje je prvo tocka, a onda i vrijednost
*/
std::pair<Matrix, double> HookJeeves::findMinimum()
{ 
  do
  {
    *Xn = explore();
   
    if ( computeF(*Xn) < computeF(*Xb) )
    {
      *Xb = *Xn;
      *Xp = (*Xn)*2 - (*Xb);
    }
    else
    {
      *dX = *dX * 0.5;
      *Xp = *Xb;
    }
    
  }while( *dX > *E );
  
  return std::make_pair(*Xb, computeF(*Xb));  
}
#endif
