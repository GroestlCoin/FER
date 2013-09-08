/* Implementacija razreda CompressionFunction - kompresijska funkcija */

#include "compression.h"

CompressionFunction *CompressionFunction::_instance = NULL; //za singleton

/** Static metoda koja pokrece kompresijsku funkciju
    
    Metoda daje izlaz kompresijske funkcije.
    Metoda je samo omotac za konkretnu metodu.
    
    @param block
    Jedan blok podataka, veličine bitlen - pokazivac na prvi byte u bloku
    
    @param chain
    Drugi ulaz u kompresijsku funkciju - chain input
    
    @param bitlen
    Veličina ulaznog bloka u bitovima, tj. broj bitova u block, chain i output
    
    @param output
    Za zadani pokazivac rezervira se memorija i u niz byte-ova se sprema izlaz
    
    @return Vracaj enum tipa hashError 
*/
hashError CompressionFunction::computeOutput
  (const byte *const block, const byte *const chain, 
   int bitlen, byte **const output)
{
  if ( _instance == NULL )
    _instance = new CompressionFunction();
  if ( _instance == NULL )
    return FAIL;
  return _instance->output(block, chain, bitlen, output);
}

/** Metoda koja racuna kompresijsku funkciju
    
    Metoda daje izlaz kompresijske funkcije.
    
    @param block
    Jedan blok podataka, veličine bitlen - pokazivac na prvi byte u bloku
    
    @param chain
    Drugi ulaz u kompresijsku funkciju - chain input
    
    @param bitlen
    Veličina ulaznog bloka u bitovima, tj. broj bitova u block, chain i output
    
    @param output
    Za zadani pokazivac u niz byte-ova se sprema izlaz, memorija mora
    prije biti rezervirana
    
    @return Vracaj enum tipa hashError 
*/
hashError CompressionFunction::output
  (const byte *const block, const byte *const chain, 
   int bitlen, byte **const output)
{
  hashError e;

  if (*output == NULL)
    return FAIL;

  byte* inputP = new byte[ (bitlen-1)/8 + 1 ];
  if ( inputP == NULL )
    return FAIL;

  byte *outputP = new byte[ (bitlen-1)/8 + 1 ];
  byte *outputQ = new byte[ (bitlen-1)/8 + 1 ];

  if ( outputP == NULL || outputQ == NULL )
    return FAIL;

  //Prvo se blok poruke i chain input naprave XOR
  //U P permutaciju ulazi taj rezultat, u Q permutaciju ulazi cisti blok poruke
  //Na kraju se napravi XOR izmedju izlaza P, Q i chain_inputa
  e = XOR::compute(block, chain, &inputP, bitlen);
  if ( e != SUCCESS )
    return e;
  
  e = Permutations::P(inputP, &outputP, bitlen);
  if ( e != SUCCESS )
    return e;
  
  e = Permutations::Q(block, &outputQ, bitlen);
  if ( e != SUCCESS )
    return e;
  
  e = XOR::compute(outputP, chain, output, bitlen);
  if ( e != SUCCESS )
    return e;
  
  e = XOR::compute(*output, outputQ, output, bitlen);
  if ( e != SUCCESS )
    return e;
  
  delete[] inputP;
  delete[] outputQ;
  delete[] outputP;
  return SUCCESS;
}

void CompressionFunction::printBytes(const byte *const msg, int len)
{
  for(int i = 0 ; i < len /8 ; ++i)
    printf("%02x", msg[i]);
  printf("\n");
}