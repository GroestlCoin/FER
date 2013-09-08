/* 
Deklaracija razreda Matrix
Razred Matrix koristi se kao apstrakcija za matrice 
Dino Santl
*/
#ifndef MATRIX_CLASS
#define MATRIX_CLASS

#include<cstdio>
#include<cstring>
#include<cmath>
#include<algorithm>

class Matrix
{
  private:
    double **mat;                                   //zapis matrice
    int R;                                          //broj redaka
    int S;                                          //broj stupaca
    void aloc(int, int);                           //alociranje memorije
    bool isZero(double);                           //ako je broj jako blizu 0  
    void swapRows(int, int);                       //zamjena redaka
    struct doublePointer                            //pomocna struktura
    {
      double* pnt;
      int S;
      double& operator[](int);
    };
   
  public:
    Matrix();                                       //konstruktor
    Matrix(int, int);                              //konstruktor
    Matrix(const Matrix&);                         //copy konstruktor
    Matrix(int, int, int);                         //konstruktor s dijagonalom
    ~Matrix();                                      //destruktor
    
    Matrix& operator=(const Matrix&);             //operator pridruzivanja
    doublePointer operator[](int)const;           //operator dohvacanja
    Matrix operator +(const Matrix&);             //operator zbrajanja
    Matrix operator -(const Matrix&);             //operator oduzimanja
    Matrix operator *(const Matrix&);             //operator mnozenja
    Matrix operator *(const double&)const;       //operator mnozenja skalarom
    Matrix& operator +=(const Matrix&);           //operator zbrajanja na sebe
    Matrix& operator -=(const Matrix&);           //operator zbrajanja na sebe
    Matrix operator *();                           //operator transponiranja
    bool operator ==(const Matrix&);              //operator jednakosti
    
    int getR()const;                              //dohvacanje redaka
    int getC()const;                              //dohvacanje stupaca
    void loadFromFile(const char*);               //ucitavanje iz datoteke
    void print();                                  //ispis na stdout
    void print(const char *);                     //ispis u datoteku

    Matrix subFG(Matrix b);                         //substitucija unaprijed
    Matrix subFG(Matrix b, Matrix P);               //substitucija unaprijed s P
    
    Matrix subBG(Matrix b);                         //substitucija unatrag
    
    void LU(void);                                 //LU dekompozicija
    Matrix LUP(void);                              //LUP dekompozicija
    
    Matrix inverse(void);                          //inverz matrice 
};

/*
throw documentation:
3 - krivo indeksirani redak
4 - neuspjela alokacija memorije
5 - greska u zapisu datoteke s matricom (krivi format)
6 - datoteka se ne moze otvoriti
8 - dimenzije matrica nisu dobre
9 - dijeljenje s 0
*/
#endif
