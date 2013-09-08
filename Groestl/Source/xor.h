/* Funkcija XOR, prilagođena za rad s tipom byte - zaglavlje razreda XOR */

#ifndef H_XOR
#define H_XOR

#include <cstdio>
#include <cstring>

#include "struct.h"

class XOR
{
  public:
    static hashError compute(const byte *const, const byte *const, 
                             byte **const, int bitlen);
};

#endif