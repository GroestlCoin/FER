/* Ljuska algoritma, zaglavlje razreda Hash */

/*
  Konvencija pisanja koda:
    - sve sto je iz std se pise std::nesto
    - imena razreda pocinju velikim slovom
    - imena metoda pocinju malim slovom, unutar imena velikim npr. setBigNumber
    - privatni atributi imaju _ ispred imena, npr. _value
    - sve poruke koje se moraju dati su na engleskom jeziku    
    - ako je samo jedan objekt u sustavu mora biti singleton
    - dokumentacija se pise iznad metoda u *.cc datotekama
    - definicije konstanti za zaglavlja imaju oblik H_ + ime zaglavlja 
*/

#ifndef H_HASH
#define H_HASH

#include <cstdio>
#include <cstring>

#include "struct.h"
#include "compression.h"
#include "output.h"

class Hash
{
  private:
    static Hash *_instance;
    hashError outputHash(const byte *const, lint, byte **const, int);
    hashError padding(int, const byte *const, lint, byte **const, lint&);
    hashError initChainInput(byte **const, int, int); 
  public:
    static hashError computeHash(const byte *const, lint, byte **const, int);
    static void printBytes(const byte *const, int);
};
#endif