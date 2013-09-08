#include <cstdio>
#include <cstring>

#include "hj.cc"
#include "box.cc"

#include "function.cc"
#include "constraint.cc"

using namespace std;

int main(void)
{
  try
  {
    HookJeeves A(Sample4::getDimension(), Sample4::function, "config.txt");
    pair<Matrix, double> sol1 = A.findMinimum();
    sol1.first.print();
    printf("%lf\n", sol1.second);  
    
    printf("-----\n");
    
    Box B(Sample3::getDimension("funcparam.txt"), Sample3::function, "configBox.txt");
    B.addConstraints( Con1::setEksplicit(Sample3::getDimension("funcparam.txt") ), 
                      Con1::setImplicit()                        );
    pair<Matrix, double> sol2 = B.findMinimum();
    sol2 = B.findMinimum();
    sol2.first.print();
    printf("%lf\n", sol2.second);
  }
  catch(int a)
  {
    printf("%d\n", a);
  }
  
  return 0;
}
