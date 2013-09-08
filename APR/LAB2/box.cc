/*
Implementacija razreda Box
Predmet: Analiza i projektiranje racunalom
Autor: Dino Šantl
*/

#ifndef __BOX_MODULE
#define __BOX_MODULE

#include "box.h"
#include <cstdlib>
#include <ctime>
#include <algorithm>
#include <functional>

/*
method: checkImplicit
output(bool): Ako su sva implicitna ogranicenja zadovoljena vrati true
*/
bool Box::checkImplicit(Matrix A)
{
  int len = implicit.size();
  bool (*funcPnt)(Matrix);
  for(int i = 0 ; i < len ; ++i)
  {
    funcPnt = (bool (*)(Matrix))(implicit[i]);
    if ( !(*funcPnt)(A) ) return false;  
  }
  return true;
}

/*
method: computeCentroid
output(Matrix): Vraca centrid nad tockama simpleksa
*/
Matrix Box::computeCentroid()
{  
  Matrix ret(dimension, 1);
  
  int len = pointSet.size();
  for(int i = 0 ; i < len ; ++i)
  {
    ret = ret + pointSet[ i ];
  }
  return ret*((double)1/len);
}

/*
method: pointGenerator
comment: Generiraju se slucajne tocke simpleksa
*/
void Box::pointGenerator()
{
  srand(time(NULL));
  if ( Xc == NULL ) throw 5;
  
  int len = eksplicit.size();
  
  if ( len != dimension ) throw 7;
   
  for(int i = 0 ; i < dimension ; ++i)
  {
    if ( (*Xc)[ i ][ 0 ] < eksplicit[i].first ||
         (*Xc)[ i ][ 0 ] > eksplicit[i].second )
          throw 6;
  }
  
  if ( !checkImplicit( *Xc ) ) throw 6;
  
  for(int k = 0 ; k < 2*dimension ; ++k)
  {
    Matrix newPoint = Matrix(dimension, 1);
    for(int j = 0 ; j < dimension ; ++j)
    {
      double rnd = (double)rand() / RAND_MAX;
      double down = eksplicit[ j ].first;
      double up   = eksplicit[ j ].second;
      newPoint[ j ][ 0 ] = down + rnd*(up-down);
    }
   
   while ( !checkImplicit( newPoint ) ) 
   {
    newPoint = (*Xc+newPoint)*0.5;
   }
   
   pointSet.push_back( newPoint );
   *Xc = computeCentroid();    
  }
}

/*
method: worstPoint
output(int): Vraca se indeks najgore tocke (indeks od pointSet)
*/
int Box::worstPoint()
{
  int len = pointSet.size();
  if ( len == 0 ) throw 8;
  
  double tmp;
  int index = 0;
  double maksimum = computeF( pointSet[ 0 ] );
  
  
  for(int i = 1 ; i < len ; ++i)
  {
    tmp = computeF( pointSet[ i ] );
    if ( maksimum < tmp )
    {
      index = i;
      maksimum = tmp;
    }
  }
  return index;
}

/*
method: findMinimum
output(pair<Matrix, double>): Pronalazi minimum s ogranicenjima
*/
std::pair<Matrix, double> Box::findMinimum()
{
  if ( functionFlag == false ) throw 4;
  
  pointGenerator(); 
  
  Matrix Xh(dimension, 1), Xh2(dimension, 1);
  int intXh, intXh2;
  do
  {
    intXh = worstPoint();
    Xh = pointSet[ intXh ]; //u Xh postavi najlosju tocku
    pointSet.erase( pointSet.begin() + intXh ); //izbrisi ju iz simpleksa
    
    intXh2 = worstPoint();
    Xh2 = pointSet[ intXh2 ]; //u Xh2 postavi drugu najgoru tocku
    
    if ( (Xh - *Xc) < E ) break; //ako je uvjet zadovoljen zaustavi algoritam
    
    *Xc = computeCentroid(); //izracunaj centroid   
    *Xr = (*Xc)*(1+alfa) - (Xh)*alfa; //izracunaj reflekciju
     
    for(int i = 0 ; i < dimension ; ++i)
    {
      if ( (*Xr)[ i ][ 0 ] < eksplicit[ i ].first ) //provjeri eksplicitna
        (*Xr)[ i ][ 0 ] = eksplicit[ i ].first;
      else if ( (*Xr)[ i ][ 0 ] > eksplicit[ i ].second ) //provjeri implicitna
        (*Xr)[ i ][ 0 ] = eksplicit[ i ].second;
    }
    
    while( !checkImplicit(*Xr) )
    {
      (*Xr) = ((*Xr)+(*Xc))*0.5;  //ako implicitna ne zadovoljavaju prebaci
    }
    
    if ( computeF( *Xr ) > computeF( Xh2 ) ) //ako je jos uvijek najlosja 
      (*Xr) = ((*Xr)+(*Xc))*0.5;     
  
  pointSet.push_back(*Xr);
  
  }while( 1 );
  
  return std::make_pair( *Xc, computeF(*Xc) );
} 

/*
method: setFunction
input((double*)(Matrix)): Pokazivac na funkciju
comment: Postavlja se pokazivac na funkciju koja se minimizira
*/
void Box::setFunction(double f(Matrix))
{
  pntFunction = f;
  functionFlag = true;
}                

/*
method: memoryFree
comment: Oslobadja se zauzeta memorija
*/
void Box::memoryFree()
{
  delete Xc;
  delete Xr;
}

/*
method: memoryAlloc
input(int): Dimenzija ulaznog prostra
comment: Rezervira se memorija
*/
void Box::memoryAlloc(int N)
{
  Xc = new Matrix(N, 1);
  Xr = new Matrix(N, 1);
  if ( Xc == NULL || Xr == NULL ) throw 1; 
}         

/*
method: computeF
input(const Matrix&): Ulazni parametri za funkciju (vektor) 
output(double): Izlaz funkcije za ulazne parametre
*/
double Box::computeF(const Matrix& M)
{
  if (functionFlag == false) throw 2;
  return (*pntFunction)(M); 
}

/*
method: setDefault
input(int): Dimenzija ulaznog prostora
comment: Postavlja pretpostavljene parametre algoritma
*/
void Box::setDefault(int N)
{
  alfa = 1.3;
  E = 1E-9;
  memoryAlloc(N);   
  for(int i = 0 ; i < N ; ++i)
  {
    (*Xc)[ i ][ 0 ] = 0;
  }
}           

/*
constructor: Box
input(const char* fileName): Ime datoteke
comment: Ulazni parametri se ucitavaju iz datotke 
*/
Box::Box(const char* fileName)
{
  functionFlag = false;
  loadFromFile(fileName, false);
}

/*
constructor: Box
input(int): Dimenzija ulaznog prostora
comment: Ulazni parametri se podrazumijevaju 
*/
Box::Box(int N)
{
  functionFlag = false;
  dimension = N;
  setDefault(N);
}

/*
constructor: Box
input(int, (double*)(Matrix)): Dimenzija ulaznog prostora, pokazivac na funkciju
comment: Parametri algoritma su podrazumijevani
*/
Box::Box(int N, double F(Matrix))
{
  functionFlag = true;
  pntFunction = F;
  dimension = N;
  setDefault(N);
}

/*
constructor: Box
input(int, (double*)(Matrix), const char*): 
                               Dimenzija ulaznog prostora, 
                               pokazivac na funkciju,
                               ime datoteke
comment: Parametri algoritma se ucitavaju iz datoteke
*/
Box::Box(int N, double F(Matrix), const char* fileName)
{
  functionFlag = true;
  pntFunction = F;
  dimension = N;
  loadFromFile(fileName, false);
  if ( dimension != N ) throw 3;
}

/*
destructor: ~Box
comment: Oslobadja memoriju 
*/
Box::~Box()
{
  memoryFree();
}

/*
method: addConstraints
input(vector<std::pair<double, double> >, vector<void*>): Eksplicitna i       
                                                          implicitna ogranicenja
comment: Dodaju se ogranicenja koja su zapisana u strukturi tako da eksplicitna
        imaju donju i gornju granicu, a implicitna su pokazivaci na funkcije
*/              
void Box::addConstraints(std::vector<std::pair<double, double> > E, 
                         std::vector<void*> I)
{
  eksplicit = E;
  implicit  = I;
}

/*
method: loadFromFile
input(const char* fileName, bool free): Ime datoteke, oslobadanje memorije
comment: Metoda ucitava parametre iz datoteke
*/
void Box::loadFromFile(const char* fileName, bool free)
{
  int N;
  double tmp;
  double ALFA, ERR;
  FILE *f = fopen(fileName, "r");
  
  fscanf(f, "%d", &N);
  fscanf(f, "%lf", &ALFA); 
  fscanf(f, "%lf", &ERR); 
   
  dimension = N;
  alfa = ALFA;
  E = ERR;
  
  if ( free ) memoryFree();  
  memoryAlloc(N);
  
  for(int i = 0 ; i < N ; ++i)
  {
    fscanf(f, "%lf", &tmp);
    (*Xc)[ i ][ 0 ] = tmp;
  }
  fclose(f);
}

#endif
