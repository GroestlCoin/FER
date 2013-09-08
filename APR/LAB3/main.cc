#include<cstdio>
#include<cstring>
#include "matrix.h"
#include "differential.h"

using namespace std;

int main(void)
{
  printf("Na stderr se ispisuju podatci ptrebni za crtanje!");
  //algoritam i frekvencija ispisa
  int choice;
  int steps;
  
  //imena datoteka
  char matrixA[256]; //= "A2.txt";
  char matrixB[256]; //= "B.txt";
  char matrixX0[256]; //= "X02.txt";
  char param[256]; // = "param1.txt";
  
  printf("Odaberite:\n1 za trapezni postupak\n2 za Runge-Kutta\n");
  scanf("%d", &choice);
  
  printf("Nakon koliko koraka se rezultat ispisuje na ekran:\n");
  scanf("%d", &steps);
  
  printf("Navedite redom imena datoteka za:\nmatrica A, matrica B, matrica pocetnog uvijet, parametri\n");
  
  scanf("%s", matrixA); 
  scanf("%s", matrixB); 
  scanf("%s", matrixX0); 
  scanf("%s", param); 
  
  try
  {
    DiffCompute *Alg;
    if (choice==1) 
      Alg = new Trapez(steps, matrixA, matrixB, matrixX0, param);
    else
      Alg = new RungeKutta(steps, matrixA, matrixB, matrixX0, param);
    Alg->Run();
  }
  catch(int a)
  {
    printf("Error: %d\n", a);
  }
  return 0;
}
