/* Implementacija razreda Hash - ljuske algoritma */

#include "hash.h"

Hash *Hash::_instance = NULL; //pokazivac na razred Hash (singleton)

/** Static metoda koja pokrece proces generiranja hash-a
    
    Ako objekt singleton razreda nije stvoren ova metoda ga stvara, i poziva 
    nad njim metodu koja generira hash
    
    @param data
    Zapis podataka u bytovima za koje se izracunava hash funkcija
    
    @param datasize
    Velicina podataka u bitovima
    
    @param hashout
    Izlazni parametar u koji ce se zapisati hash
    
    @param hashsize
    Velicina hasha u bitovima, koji zadaje korisnik
    
    @return Vracaj enum tipa hashError 
*/
hashError Hash::computeHash(const byte *const data, lint datasize, 
                            byte **const hashout, int hashsize)
{
  if ( _instance == NULL )
    _instance = new Hash();
  if ( _instance == NULL )
    return FAIL;
  return _instance->outputHash(data, datasize, hashout, hashsize);
}

/** Metoda koja predstavlja ljusku algoritma
    
    Ova metoda obavlja sav posao, pozivajući objekte da odrade svoj posao
    
    @param data
    Zapis podataka u bytovima za koje se izracunava hash funkcija.
    Rezervira se memorija za hashout, ako je rezervirana memorija, onda se 
    oslobadja.
    
    @param datasize
    Velicina podataka u bitovima
    
    @param hashout
    Izlazni parametar u koji ce se zapisati hash
    
    @param hashsize
    Velicina hasha u bitovima, koji zadaje korisnik
    
    @return Vracaj enum tipa hashError 
*/
hashError Hash::outputHash(const byte *const data, lint databitsize, 
                           byte **const hashout, int hashbitsize)
{
  
  if (hashbitsize <= 0 || (hashbitsize%8) || hashbitsize > 512)
    return BAD_HASHLEN;

  byte* out;    //prosireni ulazni podaci, izlaz iz padding funkcije
  lint outsize; //velicina podataka u bitovima od padding funkcije
  int version; //velicina bloka poruke, tj. koje verzije koristenih matrica
  lint blocknum; //broj blokova
  byte* chain_input; //drugi ulaz u blok (velicine version), za prvi blok je def
  hashError e; //varijala za detekciju greske

  //koliki ce biti l, on je ovisan o duljini hasha
  if ( hashbitsize <= 256 )  
    version = 512;
  else
    version = 1024;
  
  e = padding(version, data, databitsize, &out, outsize);
  if ( e != SUCCESS ) 
    return e;
  
  //pocetna vrijednost chain inputa je l-bitna reprezentacija duljine hasha
  //l i version su istoznacnice
  e = initChainInput(&chain_input, version, hashbitsize);
  if ( e != SUCCESS ) 
    return e;
  
  blocknum = outsize / version;
  for(int i = 0 ; i < blocknum ; ++i)
  {
    //metoda racuna izlaz funkcije, a rezultat opet sprema u chain_input
    //jer je to novi ulaz za slijedeci blok
    e = CompressionFunction::computeOutput(out+i*(version/8), chain_input, 
                                           version, &chain_input);
    if ( e != SUCCESS ) 
      return e;
  }

  //chain_input je ulaz za izlaznu funkicju  
  e = OutputFunction::computeOutput(chain_input, version, hashout, hashbitsize);
  if ( e != SUCCESS ) 
    return e;
  
  delete[] out;
  delete[] chain_input;
  return SUCCESS;
}

/** Metoda definira prvi chain ulaz, tj. chain ulaz u prvi blok
    
    Metoda puni ulaz s bitovnim prikazom broja number, ukupna duljina
    izlaznog niza je len. VAZNO: za parametar chain_input memorija se 
    rezervira u toj metodi, ali duznost onoga tko poziva 
    je da tu memoriju oslobodi.
    
    @param chain_input
    Pokazivac koji ce pokazivati na prvi byte inicjalne vrijednosti

    @param len
    Broj bitova od chain_input-a
    
    @param number
    Broj koji ce se pretvoriti u niz bitova

    @return Vracaj enum tipa hashError
*/
hashError Hash::initChainInput(byte **const chain_input, int len, int number)
{
  *chain_input = new byte[ (len-1)/8+1 ];
  
  if ( *chain_input == NULL )
    return FAIL;
  
  memset( *chain_input, 0, ((len-1)/8 + 1) * sizeof(byte));
  
  for(int i = 0 ; i < 4; ++i)
  {
    (*chain_input)[ (len-1)/8-i ] = (number>>(i*8)) & 0xff;
  }
  return SUCCESS;
}

/** Metoda koja prosiruje ulaz u hash funkciju, tako da je duzina dijeljiva s l
    
    Obraduje se ulazni niz tako da duzina na izlazu bude dijeljiva s l, tj.
    segmentom poruke koja ide u jedan blok kompresijske funkcije. Cilj je osim
    toga, zapisati duljinu novog niza u zadnjih 64-bita, sto nema svrhu u 
    programskoj izvedbi, ali jako korisna stvar u sklopovskoj.
    Ako je duljina poruke veca od 2^73-577 i l = 512 ne jamci se dobar rezultat.
    Isto tako ako je l = 1024 ulaz ne smije biti dulji od 2^74-1089
    VAZNO: za parametar output memorija se 
    rezervira u toj metodi, ali duznost onoga tko poziva 
    je da tu memoriju oslobodi.

    @param l
    Velicina jednog bloka podataka (jedan blok - jedna kompresijska funkcija)
    
    @param input
    Zapis podataka u bytovima za koje se izracunava hash funkcija.
    Rezervira se memorija za hashout, ako je rezervirana memorija, onda se 
    oslobadja.
    
    @param inputsize
    Velicina ulaznog niza bitovima
    
    @param output
    Izlazni niz, tj. prosireni
    
    @param outputsize
    Velicina izlaznog niza u bitovima, taj broj sigurno je dijeljiv s l
    
    @return Vracaj enum tipa hashError
*/
hashError Hash::padding(int l, const byte *const input, lint inputsize,
                        byte **const output, lint& outputsize)
{
  int w = l-(inputsize+65)%l; //koliko 0 treba dodati da niz bude dijeljiv s l
  outputsize = inputsize + 65 + w;  
  
  *output = new byte[ (outputsize-1)/8 + 1 ];
  
  if (*output == NULL)
    return FAIL;

  memset(*output, 0, ((outputsize-1)/8 + 1)*sizeof(byte));
  
  int bit_count = inputsize+1; //trenutni bit u koji se zapisuje iza ulaza
  
  for(int i = 0 ; i < (inputsize-1)/8 + 1; ++i)
  {
    (*output)[i] = input[ i ]; //kopiranje po byte-ovima!
  }
  (*output)[ (bit_count-1)/8 ] |= 1<<(7-(bit_count-1)%8); //dodavanje 1 na kraj
  
  //kako su inicjalno sve 0, ne treba puniti s nulama opet
  //punimo zadnjih 64 bita s reprezentacijom broj (N+w+65)/l  

  bit_count += w + 1; //do tog bita su sve 0
  
  lint module_no = outputsize / l; //zadnjih 64 bita

  //bit po bit
  for(int i = 0 ; i < 64 ; ++i, bit_count += 1)
  {
    (*output)[ (bit_count-1)/8 ] |= (1&((module_no)>>(63-i)))
                                    <<(7-(bit_count-1)%8);
  }
  
  return SUCCESS;
}

void Hash::printBytes(const byte *const msg, int len)
{
  for(int i = 0 ; i < len /8 ; ++i)
    printf("%02x", msg[i]);
  printf("\n");
}
