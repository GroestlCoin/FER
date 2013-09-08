/* Funkcije permutacija, zaglavlja razreda za permutacije */

#ifndef H_PERMUTATIONS
#define H_PERMUTATIONS

#include <cstdio>
#include <cstring>

#include "struct.h"
#include "xor.h"

//Optimizirano mnozenje nad GF(2^8)
#define mul1(b) ((byte)(b))
#define mul2(b) ((byte)((b)>>7?((b)<<1)^0x1b:((b)<<1)))
#define mul3(b) (mul2(b)^mul1(b))
#define mul4(b) mul2(mul2(b))
#define mul5(b) (mul4(b)^mul1(b))
#define mul6(b) (mul4(b)^mul2(b))
#define mul7(b) (mul4(b)^mul2(b)^mul1(b))

class BasePermutation
{
  protected:
      static const byte s_box[16][16]; //s-box matrica substitucije
      byte input[8][16]; //ulazna matrica
      int shift[8]; //posmak u redu
      const unsigned int R; //broj permutacija
      const unsigned int COL; //broj stupaca matrice
      
      virtual hashError output(const byte *const, byte **const);
      virtual void addRoundConstant(unsigned int) = 0;
      virtual void subBytes();
      virtual void shiftBytes();
      virtual void mixBytes();
      void printMatrix();           
  public:
    BasePermutation(unsigned int, unsigned int);
};

class P512 : public BasePermutation
{
  private:
    static P512* _instance;
    virtual void addRoundConstant(unsigned int);
    P512();
     
  public:
    static hashError computeOutput(const byte *const, byte **const);
};

class Q512 : public BasePermutation
{
   private:
    static Q512* _instance;
    virtual void addRoundConstant(unsigned int);
    Q512();

  public:
    static hashError computeOutput(const byte *const, byte **const);
};

class P1024 : public BasePermutation
{
   private:
    static P1024* _instance;
    virtual void addRoundConstant(unsigned int);
    P1024();

  public:
    static hashError computeOutput(const byte *const, byte **const);
};

class Q1024 : public BasePermutation
{
   private:
    static Q1024* _instance;
    virtual void addRoundConstant(unsigned int);
    Q1024();

  public:
    static hashError computeOutput(const byte *const, byte **const);
};

class Permutations
{
  public:
    static hashError P(const byte *const, byte **const, int);
    static hashError Q(const byte *const, byte **const, int);
};

#endif