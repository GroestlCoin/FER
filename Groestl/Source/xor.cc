/* Implementacija razreda XOR */

#include "xor.h"

/** Static metoda koja racuna XOR izmedju dva niza byte-ova duljine bitlen
    
    Razred predstavlja servis koji ima jednu staticnu metodu compute za 
    racunanje xor operacije izmedju dva niza byte-ova. Rezultat se sprema
    u treci argument, te prije potrebno rezervirati memoriju.

    @param opr1
    Prvi operand (pokazivac na prvi byte u nizu)

    @param opr2
    Drugi operand (pokazivac na prvi byte u nizu)

    @param res
    Rezultat operacije xor

    @param bitlen
    Broj bitova nad kojima se radi operacija
    
    @return Vracaj enum tipa hashError 
*/
hashError XOR::compute(const byte *const opr1, const byte *const opr2, 
                             byte **const res, int bitlen)
{
  if ( *res == NULL )
    return FAIL;
  
  for(int i = 0 ; i < (bitlen-1)/8 + 1 ; ++i)
  {
    (*res)[ i ] = opr1[ i ] ^ opr2[ i ];
  }

  return SUCCESS;
}