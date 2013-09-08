CC = g++
CFLAGS = -Wall -g
COPTFLAGS = -O2

PROJECT = groestl
MAIN = main.cc
HEADERS = struct.h xor.h hash.h compression.h output.h permutations.h
SRC = xor.cc hash.cc compression.cc output.cc permutations.cc
OBJECTS = ${SRC:.cc=.o}

all: $(MAIN) $(HEADERS) $(OBJECTS)
	$(CC) $(CFLAGS) $(OBJECTS) $(MAIN) -o $(PROJECT)

opt: $(MAIN) $(HEADERS) $(SRC)
	$(CC) $(COPTFLAGS) $(SRC) $(MAIN) -o $(PROJECT)

$(OBJECTS): $(SRC) $(HEADERS)

clean:
	rm -f $(OBJECTS) $(PROJECT)
	
