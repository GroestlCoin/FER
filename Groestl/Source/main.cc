/* Glavni program Groestl hasing algoritma
   Autor implementacije: Dino Šantl - dino.santl@fer.hr, dsantl.ck@gmail.com
   Fakultet elektrotehnike i racunarstva
   2013.
*/ 

#include <cstdio>
#include <cstdlib>
#include <cstring>

#include "hash.h"

void Error(const char *msg)
{
  fprintf(stderr, "%s\n", msg);
  exit(1);
}

int main(int agrc, const char** argv)
{

  if ( agrc < 3 )
  {
    fprintf(stderr, "Usage: %s file version\n", argv[0]);
    return 1;
  }

  byte *buff;
  char fileName[1024];
  long int fileSize;

  byte *hash;
  int version;

  sscanf(argv[2], "%d", &version);
  sscanf(argv[1], "%s", fileName);

  FILE *file = fopen(fileName, "rb");
  
  if ( file == NULL) 
    Error("File open error!\n");
  
  fseek(file, 0, SEEK_END);
  fileSize = ftell(file);
  rewind(file);

  buff = new byte[fileSize];

  if ( buff == NULL )
    Error("Not enough memory!\n");
  
  fread (buff, sizeof(byte), fileSize, file);
  fclose(file);

  hashError e = Hash::computeHash(buff, fileSize*8, &hash, version);
  
  if ( e != SUCCESS )
    Error("Hash FAIL\n");
  else
    Hash::printBytes(hash, version);
  
  delete[] hash;
  return 0;
}
