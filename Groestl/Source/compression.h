/* Funkcija kompresije, zaglavlje razreda CompressionFunction */

#ifndef H_COMPRESSION
#define H_COMPRESSION

#include <cstdio>
#include <cstring>

#include "struct.h"
#include "xor.h"
#include "permutations.h"

class CompressionFunction
{
  private:
    static CompressionFunction *_instance;
    hashError output(const byte *const, const byte *const,  int, byte **const);
    void printBytes(const byte *const, int);
  public:
    static hashError computeOutput(const byte *const, const byte *const, 
                                   int, byte **const);
};
#endif
