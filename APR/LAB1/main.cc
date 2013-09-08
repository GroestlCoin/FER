/*
Testiranje razreda matrix
Predmet: Analiza i projektiranje racunalom
Autor: Dino Šantl
*/

#include<cstdio>
#include<cstring>
#include<vector>

#include "matrix.cc"

using namespace std;

int main(void)
{
  try
  {
    Matrix A;
    A.loadFromFile("test1.txt");
    A.LU();
    A.print();
  }
  catch(int a)
  {
    printf("Error: %d\n", a);
  }
  
  return 0;
}
