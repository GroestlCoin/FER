#include <cstdio>
#include <cstring>

#include "Groestl-intermediate.h"

using namespace std;


int main(int argc, char **argv)
{

  BitSequence str[1000000];
  BitSequence hashval[1024];
  int version;
  
  FILE *file = fopen(argv[1], "rb");
  
  fseek(file, 0, SEEK_END);
  long int fileSize = ftell(file);
  rewind(file);

  fread (str, sizeof(BitSequence), fileSize, file);
  fclose(file);

	sscanf(argv[2], "%d", &version);
  
  Hash(version, str, strlen((char*)str)*8, hashval);
	PrintHash(hashval, version);

  return 0;
}
