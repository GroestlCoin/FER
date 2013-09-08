/*
Implementacija razreda Matrix
Predmet: Analiza i projektiranje racunalom
Autor: Dino Šantl
*/

#ifndef __MATRIX_MODULE
#define __MATRIX_MODULE 

#include<cstdio>
#include<cstring>
#include<cmath>
#include "matrix.h"

/*
operator: >
input(int): Index stupca koji se adresira
output(dobule&) Vraca se referenca na element u matrici 
*/
bool Matrix::operator<(double E)
{
  for(int i = 0 ; i < this->getR() ; ++i)
    for(int j = 0 ; j < this->getC() ; ++j)
      if ( std::abs(mat[ i ][ j ]) > E ) return false; 
  return true;
}

/*
operator: >
input(int): Index stupca koji se adresira
output(dobule&) Vraca se referenca na element u matrici 
*/
bool Matrix::operator>(const Matrix &M)
{
  if ( this->getR() != M.getR() || this->getC() != M.getC() ) return false;
  
  for(int i = 0 ; i < this->getR() ; ++i)
    for(int j = 0 ; j < this->getC() ; ++j)
      if ( M[ i ][ j ] > mat[ i ][ j ] ) return false;  
      
  return true;
}


/*
operator: []
input(int): Index stupca koji se adresira
output(dobule&) Vraca se referenca na element u matrici 
*/
double& Matrix::doublePointer::operator[](int index)
{
  if ( index >= S ) throw 3;
  return pnt[ index ];
}

/*
method: isZero
input(double): Broj tipa double
output(bool): Vraca true ako se ulazni parametar smatra da je 0
*/
bool Matrix::isZero( double x )
{
  if ( std::abs(x) < 0.0001 ) return true;
  return false;
}

/*
constructor: Matrix
input(int, int, int): R-broj redaka, S-broj stupaca, D-inicjalna vrijednost
comment: Stvara se matrica RxS, gdje su elementi na dijagonali jednaki D 
*/
Matrix::Matrix(int R, int S, int D)
{
  aloc(R, S);
  for(int i = 0 ; i < R ; ++i)
    mat[ i ][ i ] = D;  
}

/*
method: swapRows
input(int, int): Oznake redaka koji se zele zamijeniti
comment: U matrici se zamijene dva retka
*/
void Matrix::swapRows(int a, int b)
{
  for(int i = 0 ; i < this->S ; ++i)
    std::swap( mat[ a ][ i ], mat[ b ][ i ] );
}


/*
method: isZero
output(Matrix): Permutacijska matrica
comment: LUP dekompozicija matrice
*/
Matrix Matrix::LUP(void)
{
  //baci iznimku ako matrica nije kvadratna
  if ( this->R != this->S ) throw 8;
  
  int swapRowBuff;
  
  //pocetna permutacijska matrica koja se i vraca
  Matrix ret(this->R, this->R, 1);
  
  for(int k = 0 ; k < this->R - 1; ++k)
  {
    this->print();
    printf("\n");
    //trenutno nista ne mijenjamo, swap(k, k)
    swapRowBuff = k;
    
    //nalazimo najveci element u stupcu k
    for(int t = k + 1 ; t < this->R ; ++t)
      if ( std::abs(mat[ t ][ k ]) > std::abs(mat[ swapRowBuff ][ k ]) ) 
        swapRowBuff = t;
    
    //ako moramo zamijeniti onda to napravimo
    if ( swapRowBuff != k ) 
    {
      this->swapRows( swapRowBuff, k );
      ret.swapRows( swapRowBuff, k );
    }
    
    //dekompozicija
    for(int i = k + 1 ; i < this->R ; ++i)
    {
      //ako je element 0, tada je matrica singularna
      if ( isZero( mat[ k ][ k ] ) ) throw 9;
      
      mat[ i ][ k ] /= mat[ k ][ k ];
      
      for(int j = k + 1 ; j < this->R ; ++j)
        mat[ i ][ j ] -= mat[ i ][ k ] * mat[ k ][ j ];
    }
  }
  
  return ret;
}

/*
method: LU
comment: LU dekompozicija koja se vrsi na matrici
*/
void Matrix::LU(void)
{
  //ako matrica nije kvadratna baci iznimku
  if ( this->R != this->S ) throw 8;
  
  //dekompozicija
  for(int k = 0 ; k < this->R - 1; ++k)
    for(int i = k + 1 ; i < this->R ; ++i)
    {
      //ako je pivot 0, nemoguce je dovrsiti LU dekompoziciju
      if ( isZero( mat[ k ][ k ] ) ) throw 9;
      mat[ i ][ k ] /= mat[ k ][ k ];
      
      for(int j = k + 1 ; j < this->R ; ++j)
        mat[ i ][ j ] -= mat[ i ][ k ] * mat[ k ][ j ];
    }
}

/*
operator: ==
input(const Matrix&): Broj tipa double
output(bool): Vraca true ako su dvije matrice jednake 
              (operator usporedbe je isZero)
*/
bool Matrix::operator ==(const Matrix& M)
{
  //ako su matrice razlicitih dimenzija nisu jednake
  if ( M.getR() != this->getR() || M.getC() != this->getC() ) return false;
  
  for(int i = 0 ; i < R ; ++i)
    for(int j = 0 ; j < S ; ++j)
      if ( !isZero( mat[ i ][ j ] - M[ i ][ j ] ) ) return false;
  return true;
} 

/*
operator: +
input(const Matrix&): Zbroji dvije matrice po definiciji
output(Matrix): Rezultat je zbroj matrica
*/
Matrix Matrix::operator +(const Matrix& M)
{
  if ( this->R != M.getR() || this->S != M.getC() ) throw 8;
  
  Matrix ret(R, S);
  
  for(int i = 0 ; i < this->R ; ++i)
    for(int j = 0 ; j < this->S ; ++j)
      ret[ i ][ j ] = mat[ i ][ j ] + M[ i ][ j ];
  
  return ret;  
}

/*
method: subBG
input(Matrix): Vektor stupac
output(Matrix): Vektor stupac u kojemu je rezultat supstitucije unatrag
comment: supstitucija unatrag
*/
Matrix Matrix::subBG(Matrix b)
{
  //ako dimenzije ne odgovaraju baci iznimku
  if ( this->R != b.getR() || b.getC() != 1 ) throw 8;
  if ( this->R != this->S ) throw 8;
  
  for(int i = this->R - 1 ; i >= 0 ; --i)
  {
    for(int j = this->R - 1 ; j > i ; --j)
    {
      b[ i ][ 0 ] -= mat[ i ][ j ] * b[ j ][ 0 ]; 
    }
    //ako je element s kojim s djeli jednak 0 baci iznimku
    if ( isZero( mat[ i ][ i ] ) ) throw 9; 
    b[ i ][ 0 ] /= mat[ i ][ i ]; 
  }
  return b; 
}

/*
method: subFG
input(Matrix): Vektor stupac
output(Matrix): Vektor stupac u kojemu je rezultat supstitucije unaprijed
comment: supstitucija unaprijed
*/
Matrix Matrix::subFG(Matrix b)
{
  //ako dimenzije matrica ne odgovaraju baci iznimku
  if ( this->R != b.getR() || b.getC() != 1 ) throw 8;
  if ( this->R != this->S ) throw 8;
  
  
  for(int i = 1 ; i < this->R ; ++i)
    for(int j = 0 ; j < i ; ++j)
    {
      b[ i ][ 0 ] -= mat[ i ][ j ] * b[ j ][ 0 ];
    }
  return b;
}

/*
method: subFG
input(Matrix, Matix): Vektor stupac, permutacijska matrica
output(Matrix): Vektor stupac u kojemu je rezultat supstitucije unaprijed
comment: supstitucija unaprijed s permutacijskom matricom
*/
Matrix Matrix::subFG(Matrix b, Matrix P)
{
  b = P * b;
  return subFG(b);
}

/*
operator: -
input(const Matrix&): Matrica
output(Matrix): Rezultat oduzimanja dviju matrica
comment: Oduzimanje matrica po definiciji
*/
Matrix Matrix::operator -(const Matrix& M)
{
  //ako dimenzije ne odgovaraju baci iznimku
  if ( this->R != M.getR() || this->S != M.getC() ) throw 8;
  
  Matrix ret(R, S);
  
  ret = *this + M*(-1);
  
  return ret;  
}

/*
operator: *
input(const double&): Broj tipa double
output(Matrix): Matrica koja je pomnozena skalarem
*/
Matrix Matrix::operator *(const double& K)const
{  
  Matrix ret(R, S);
  
  for(int i = 0 ; i < this->R ; ++i)
    for(int j = 0 ; j < this->S ; ++j)
      ret[ i ][ j ] = K*mat[ i ][ j ];
  
  return ret;  
}

/*
operator: *
input(const Matrix&): Matrica
output(Matrix): Umnozak matrica
*/
Matrix Matrix::operator *(const Matrix& M)
{
  //ako dimenzije ne odgovaraju baci iznimku
  if ( this->S != M.getR() ) throw 8;
  
  Matrix ret(this->R, M.getC());
  
  //Mnozenje matrica u O(n^3)
  for(int i = 0 ; i < this->R ; ++i)
    for(int j = 0 ; j < M.getC() ; ++j)
    {
      ret[ i ][ j ] = 0; 
      for(int k = 0 ; k < this->S ; ++k)
        ret[ i ][ j ] += mat[ i ][ k ] * M[ k ][ j ];
    }
  return ret;  
}

/*
operator: +=
input(const Matrix&): Matrica
output(Matrix&): Zbroj matrica
*/
Matrix& Matrix::operator +=(const Matrix& M)
{
  //ako dimenzije ne odgovaraju baci iznimku
  if ( this->R != M.getR() || this->S != M.getC() ) throw 8;
  *this = *this + M;
  return *this;
} 

/*
operator: -=
input(const Matrix&): Matrica
output(Matrix): Razlika matrica
*/
Matrix& Matrix::operator -=(const Matrix& M)
{
  //ako dimenzije ne odgovaraju baci iznimku
  if ( this->R != M.getR() || this->S != M.getC() ) throw 8;
  *this = *this - M;
  return *this;
}

/*
operator: *
output(Matrix): Transponirana matrica
*/
Matrix Matrix::operator *()
{
  Matrix ret(S, R);
  
  for(int i = 0 ; i < R ; ++i)
    for(int j = 0 ; j < S ; ++j)
      ret[ j ][ i ] = mat[ i ][ j ]; 
  
  return ret;
}

/*
method: aloc
input(int, int): Rezervacija memorije za matricu int x int
*/
void Matrix::aloc(int A, int B)
{
  R = A;
  S = B;
  mat = new double*[ R ];
  if ( mat == NULL ) throw 4;
  for(int i = 0 ; i < R ; ++i)
  {
    mat[ i ] = new double[ S ];
    if ( mat[ i ] == NULL ) throw 4;
  }
  for(int i = 0 ; i < R ; ++i)
    for(int j = 0 ; j < S ; ++j)
      mat[ i ][ j ] = 0;
}

/*
method: print
comment: Ispis matrice
*/
void Matrix::print()
{
  for(int i = 0 ; i < R ; ++i)
  {  
    for(int j = 0 ; j < S ; ++j)
      printf("%lf ", mat[i][j]);
    printf("\n");
  }  
}

/*
method: print
input(const char*): Ime datoteke
comment: Zapis matrice u datoteku
*/
void Matrix::print(const char *filename)
{
  FILE* f = fopen(filename, "w");
  if ( f == NULL ) throw 6;
  for(int i = 0 ; i < R ; ++i)
  {  
    for(int j = 0 ; j < S ; ++j)
      fprintf(f, "%lf ", mat[i][j]);
    fprintf(f, "\n");
  }
  fclose(f);  
}

/*
constructor: Matrix
input(int, int): Dimenzije matrica
*/
Matrix::Matrix(int A, int B)
{
  aloc(A, B);
}          

/*
destructor: ~Matrix
comment: Oslobadanje zauzete memorije
*/
Matrix::~Matrix()
{
  if ( R == 0 || S == 0 ) return;
  for(int i = 0 ; i < R ; ++i)
    delete[] mat[ i ];
  delete[] mat;
}                                      

/*
constructor: Matrix 
input(const double&): Broj tipa double
comment: copy constructor
*/ 
Matrix::Matrix(const Matrix& A)
{
  aloc( A.getR(), A.getC() );
  
  for(int i = 0 ; i < R ; ++i)
    for(int j = 0 ; j < S ; ++j)
      this->mat[ i ][ j ] = A[ i ][ j ];
}                         

/*
method: getR
output(int): Vrati broj redaka matrice
*/
inline int Matrix::getR()const
{
  return this->R;
}

/*
method: getC
output(int): Vrati broj stupaca matrice
*/
inline int Matrix::getC()const
{
  return this->S;
}

/*
method: loadFromFile
input(const char*): Ime datoteke
comment: Ucitava se matrica iz datoteke
*/
void Matrix::loadFromFile(const char *filename)
{
  int Rl = 0, Sl = 0;
  int tmpSl = 0;
  
  char buff[2024];
  
  FILE *f = fopen(filename, "r");
  
  //ako se datoteka ne moze otvoriti baci iznimku
  if ( f == NULL ) throw 6;
  
  //citaj tako dugo dok postoji linija 
  //prvo progledaj dimenzije matrica a onda ucitaj u matricu
  while( fgets(buff, 2024, f) != NULL )
  {
    int len = strlen( buff );
    tmpSl = 0;
    for(int i = 0 ; i < len ; ++i)
      if( buff[ i ] == ' ' || buff[ i ] == '\n' || buff[ i ] == '\t' ) 
      {
        if ( Rl == 0 ) Sl += 1;
        tmpSl += 1;
      }
    if ( tmpSl != Sl ) throw 5;
    Rl += 1;
  }
  
  aloc(Rl, Sl);
  
  //ponovo citanje iz datoteke i upis u matricu
  fseek(f, 0, SEEK_SET );
  
  for(int i = 0 ; i < R ; ++i)
    for(int j = 0 ; j < S ; ++j)
    {
      fscanf(f, "%lf", &mat[i][j] );
    }
   
  fclose(f);
  
}

/*
constructor: Matrix
comment: Stvaranje matrice bez dimenzija (nema je u memoriji)
*/
Matrix::Matrix()
{
  R = S = 0;
}

/*
operator: []
input(int): index retka matrice
output(double*): vraca se pokazivac na redak matrica
*/
Matrix::doublePointer Matrix::operator[](int index)const
{
  if ( index >= R ) throw 3;
  doublePointer ret;
  
  ret.S = this->getC(); 
  ret.pnt = mat[ index ];
  
  return ret;
}

/*
operator: =
input(cont Matrix&): Matrica
output: Matrica kojoj je pridruzena nova vrijednost
comment: Pridruzi jednu matricu drugoj
*/
Matrix& Matrix::operator=(const Matrix& A)
{
  if ( A.getR() != this->R || A.getC() != this->S ) throw 8;
  for(int i = 0 ; i < R ; ++i)
    for(int j = 0 ; j < S ; ++j)
      this->mat[ i ][ j ] = A[ i ][ j ]; 
  return *this;
}
#endif
