#!/usr/bin/env python

import sys
import random

if len(sys.argv) < 2:
	print "Usage: " + sys.argv[0] + " version"
	sys.exit(1)


version = int(sys.argv[1])
no_test = 500

for i in range(no_test):
	print "Create test: "+str(i+1)+"/"+str(no_test)
	dat = open("in"+str(i)+"_"+str(version)+".in", "w")
	msg_len = i**2 + 5
	for j in range(msg_len):
		dat.write(chr(random.randrange(ord('A'), ord('z'))))
	dat.write('\n')
	dat.close()
