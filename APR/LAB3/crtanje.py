#!/usr/bin/env python
import numpy as np
import matplotlib.pyplot as plt

datoteka=open("izlaz.txt", "r")

X=[]
Y=dict()

for i in datoteka:
  broj=i.rstrip().split(" ")
  X.append(float(broj[0]))
  for j in range(1, len(broj)):
    if (j-1) not in Y:
      Y[j-1]=[]
    Y[j-1].append(float(broj[j]))
    

plt.title("Izlaz prijelaznih pojava")
for i in Y:
  plt.plot(X, Y[i])
plt.show()

