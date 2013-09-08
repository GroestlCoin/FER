/*
Implementacija razreda Trapez i RungeKutta
Predmet: Analiza i projektiranje racunalom
Autor: Dino Šantl
*/

#include "differential.h"

/*
Konstruktor bazne klase tj. svih algoritama za racunanje diff jednadzbi
*/
DiffCompute::DiffCompute(int num, const char *fileA, const char *fileB, const char *fileX, const char *fileParam)
{
  A.loadFromFile(fileA);
  B.loadFromFile(fileB);
  X0.loadFromFile(fileX);
  loadParameters(fileParam);
  userNumOfSteps=num;
}

/*
Ucitavanje parametara algoritma
*/
void DiffCompute::loadParameters(const char *fileName)
{
  double downB, upB;
  FILE* paramDat = fopen(fileName, "r");
  fscanf(paramDat, "%lf %lf", &downB, &upB);
  interval = std::make_pair(downB, upB);
  fscanf(paramDat, "%lf", &(this->T));
  fclose(paramDat);
}

//Konstruktor trapeznog postupka//
Trapez::Trapez(int num, const char *fileA, const char *fileB, const char *fileX, const char *fileParam) : DiffCompute(num, fileA, fileB, fileX, fileParam)
{}

/*
Override metoda koja predstavlja algoritam Trapeznog postupka
*/
void Trapez::Run()
{
  Matrix I(A.getR(), A.getC(), 1);
  
  Matrix R = (I-A*0.5*T).inverse()*(I+A*0.5*T);
  Matrix S = (I-A*0.5*T).inverse()*B*0.5*T;
  
  Matrix X = X0;
  
  int cnt = 0;   
  for(double t = 0 ; t <= interval.second ; t += T, cnt+=1)
  {
    X = R*X + S;
    
    if ( t < interval.first ) continue;
    
    fprintf(stderr, "%lf ", t);
    for(int i = 0 ; i < X.getR() ; ++i)
      fprintf(stderr, "%lf ", X[i][0]);
    
    fprintf(stderr, "\n");
    
    if (cnt%userNumOfSteps==0) 
    {
      printf("X=\n");
      X.print();
      printf("----------\n");
    }
  } 
}

/*
Konstruktor RungeKutta
*/
RungeKutta::RungeKutta(int num, const char *fileA, const char *fileB, const char *fileX, const char *fileParam) : DiffCompute(num, fileA, fileB, fileX, fileParam)
{}

/*
Override metoda za RungeKutta
*/
void RungeKutta::Run()
{
  Matrix m1(X0.getR(), X0.getC());
  Matrix m2(X0.getR(), X0.getC());
  Matrix m3(X0.getR(), X0.getC());
  Matrix m4(X0.getR(), X0.getC());
  
  Matrix X = X0;
  
  int cnt = 0;   
  for(double t = 0 ; t <= interval.second ; t += T, cnt+=1)
  {
    
    m1 = F(X);
    m2 = F(X+m1*T*0.5);
    m3 = F(X+m2*T*0.5);
    m4 = F(X+m3*T);
    
    X = X + (m1+m2*2+m3*2+m4)*(T/6);
    
    if ( t < interval.first ) continue;
    
    fprintf(stderr, "%lf ", t);
    for(int i = 0 ; i < X.getR() ; ++i)
      fprintf(stderr, "%lf ", X[i][0]);
    
    fprintf(stderr, "\n");
    
    
    if (cnt%userNumOfSteps==0) 
    {
      printf("X=\n");
      X.print();
      printf("----------\n");
    }
  } 
}

/*
Racunanje derivacije
*/
Matrix RungeKutta::F(const Matrix& X)
{  
  return A*X+B;
}
