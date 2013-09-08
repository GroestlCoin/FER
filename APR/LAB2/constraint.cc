/*
Ogranicenja
Dino Santl
*/

namespace Con1
{
  
  bool imp1(Matrix A)
  {
    double X1 = A[0][0];
    double X2 = A[1][0];
    return (X1-X2 <= 0);
  }

  bool imp2(Matrix A)
  {
    double X1 = A[0][0];
    return (X1-2 <= 0);
  }
  
  std::vector<void*> setImplicit()
  {
    std::vector<void*> implicit;
    implicit.push_back( (void*)imp1 );
    implicit.push_back( (void*)imp2 );
    return implicit;
  }
  
  std::vector<std::pair<double, double> > setEksplicit(int N)
  {
    std::vector<std::pair<double, double> > eksplicit;
    for(int i = 0 ; i < N ; ++i)
      eksplicit.push_back( std::make_pair(-100, 100) );
    return eksplicit;
  }
  
}
