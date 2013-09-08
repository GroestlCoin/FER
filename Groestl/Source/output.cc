/* Implementacija izlazne funkcije - konacni izlaz hash funkcije */

#include "output.h"

OutputFunction *OutputFunction::_instance = NULL; //za singleton

/** Static metoda koja pokrece izlaznu funkciju
    
    Metoda daje izlaz izlazne funkcije.
    Metoda je samo omotac za konkretnu metodu.
    
    @param input
    Niz byte-ova koji ulazi u izlaznu funkciju

    @param inputbitlen
    Duljina u bitovima za ulazni niz

    @param out
    Za zadani pokazivac rezervira se memorija i u njega se sprema izlaz (hash)

    @param hasbitlen
    Duljina izlaza (hash-a) u bitovima 
    
    @return Vracaj enum tipa hashError 
*/
hashError OutputFunction::computeOutput
	(const byte *const input, int inputbitlen, byte **const out, int hashbitlen)
{
  if ( _instance == NULL )
    _instance = new OutputFunction();
  if ( _instance == NULL )
  	return FAIL;
  return _instance->output(input, inputbitlen, out, hashbitlen);
}

/** Metoda racuna zadnji korak u kreiranju hash-a
    
    Nakon probavljanja svih blokova, izlaz iz zadnjeg bloka funkcije kompresije
    daje se kao ulaz ovoj meotdi. Kao izlaz dobija se konacni hash.
    VAZNO: memorija za out se rezervira u ovoj metodi, nije potrebno 
    prije rezervirati.
    Prvo se izlaz iz zadnjeg bloka (tj. sada ulaz) permutira s P, nakon toga
    napravi se XOR permutiranoga ulaza i cistog ulaza, kao hash uzimaju se 
    zadnjih hasbitlen bitova dobivenog rezultata.

    @param input
    Niz byte-ova koji ulazi u izlaznu funkciju

    @param inputbitlen
    Duljina u bitovima za ulazni niz

    @param out
    Za zadani pokazivac rezervira se memorija i u njega se sprema izlaz (hash)

    @param hasbitlen
    Duljina izlaza (hash-a) u bitovima 
    
    @return Vracaj enum tipa hashError 
*/
hashError OutputFunction::output
	(const byte *const input, int inputbitlen, byte **const out, int hashbitlen)
{
  hashError e;
	*out = new byte[(hashbitlen-1)/8+1];
	if ( *out == NULL )
		return FAIL;
	memset(*out, 0, ((hashbitlen-1)/8+1)*sizeof(byte));

	byte* outputP = new byte[(inputbitlen-1)/8+1];
	if ( outputP == NULL )
		return FAIL;

	byte* outputXOR = new byte[(inputbitlen-1)/8+1];
	if ( outputP == NULL )
		return FAIL;

	e = Permutations::P(input, &outputP, inputbitlen);
	if ( e != SUCCESS )
    return e;

	e = XOR::compute(outputP, input, &outputXOR, inputbitlen);
	if ( e != SUCCESS )
    return e;
  
  int bit_count = inputbitlen - hashbitlen; //brojac za ulazni niz bitova
  
  //kopiranje bit po bit
  for(int i = 0 ; i < hashbitlen ; ++i, bit_count += 1)
  {
  	(*out)[ i/8 ] |= (1&(outputXOR[bit_count/8]>>(bit_count%8)))<<(i%8);
  }

	delete[] outputP;
	delete[] outputXOR;
  return SUCCESS;
}

void OutputFunction::printBytes(const byte *const msg, int len)
{
  for(int i = 0 ; i < len /8 ; ++i)
    printf("%02x", msg[i]);
  printf("\n");
}
