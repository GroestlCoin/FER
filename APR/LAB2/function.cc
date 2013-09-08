/*
Razred koji implementira funkcije koje se minimiziraju
Dino Santl
*/

#include <vector>

namespace Sample1
{
    const int getDimension()
    {
      return 2;
    }
    double function(Matrix A)
    {
      double X = A[0][0];
      double Y = A[1][0];
      return 10*(X*X-Y)*(X*X-Y)+(1-X)*(1-X);
    }
}

namespace Sample2
{
    int getDimension()
    {
      return 2;
    }
    
    double function(Matrix A)
    {
      double X = A[0][0];
      double Y = A[1][0];
      return (X-4)*(X-4) + 4*(Y-2)*(Y-2);
    }
}

namespace Sample3
{
  int N;
  std::vector<double> P;
  
  int getDimension(const char *fn)
  {
    P.clear();
    double num;
    FILE *f = fopen(fn, "r");
    fscanf(f, "%d", &N);
    for(int i = 0 ; i < N ; ++i)
    {
      fscanf(f, "%lf", &num);
      P.push_back(num);
    }
    fclose(f);
    return N;  
  }
  
  double function(Matrix A)
  {
    if ( (int)P.size() != N ) return 0;
    double suma = 0;
    for(int i = 0 ; i < N ; ++i)
      suma += ( A[i][0] - P[i] ) * ( A[i][0] - P[i] );
    
    return suma;    
  }   
}

namespace Sample4
{
  int getDimension()
  {
    return 2;  
  }
  
  double function(Matrix A)
  {
    double X = A[0][0];
    double Y = A[1][0];
    return std::abs((X-Y)*(X+Y)) + sqrt(X*X+Y*Y);
  }   
}

namespace Sample5
{
    const int getDimension()
    {
      return 3;
    }
    double function(Matrix A)
    {
      double X = A[0][0];
      double Y = A[1][0];
      double Z = A[2][0];
      return X*(X-1)+(Y*Y-2)+(Z*Z-3);
    }
}
