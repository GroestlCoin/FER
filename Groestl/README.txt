Organizacija mape:

Source  - izvorne datoteke programa, vlastita implementacija
        - prevodjenje izvornih datoteka:
        - make -> normalno prevodenje za debugiranje
        - make opt -> prevodenje s optimizacijom
        - make clean -> brisanje svih stvorenih datoteka prevodenjem
        - stvori se izvrsna datoteka groestl
        - argumenti se daju preko komandne linije
        - prvi agument je ime datoteke a drugi verzija (duljina) hash-a
        - primjer: ./groestl test.txt 256
        - isps programa mora biti:
        - 8d8536afb166afea95f8d5fba3bbe6eae53a11bb6d1090e8a589b3e8d4492f72

groestl - izvorne datoteke orginalnog algoritma - referenca u testiranju
        - koristi se za testiranje vlastite implementacije
        - make -> kreira se izvrsna datoteka imena groestl
        - make clean -> brisanje izvrsne datoteke
        - NAPOMENA:
        - ova implementacija je preuzeta sa stranica Groestl algoritma
        - http://www.groestl.info/

test    - skripte generator.py i tester.sh
        - generator.py generira 500 random test primjera
        - argument je duljina hash-a u bitovima
        - primjer pokretanja: generator.py 256
        - tester.sh pokrece tesiranje
        - argument je duljina hash-a u bitovima
        - primjer pokretanja: tester.sh 256
        - ukoliko se zele obrisati datoteke koje su stvorene:
        - tester.sh delete
        - NAPOMENA: prije testiranja potrebno je prevesti implementacije
        - Source i groestl 
