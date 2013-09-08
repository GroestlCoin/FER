/* Izlazna funkcija, zaglavlje razreda OutputFunction */

#ifndef H_OUTPUT
#define H_OUTPUT

#include <cstdio>
#include <cstring>

#include "struct.h"
#include "xor.h"
#include "permutations.h"

class OutputFunction
{
	private:
		static OutputFunction *_instance;
		hashError output(const byte *const, int, byte **const, int);
		void printBytes(const byte *const, int);
	public:
		static hashError computeOutput(const byte *const, int, byte **const, int);
};

#endif