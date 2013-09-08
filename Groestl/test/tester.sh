#!/bin/bash

if [ "$1" = "" ]
then
	echo "Usage: $0 [version|delete]"
	exit 0
fi

if [ "$1" = "delete" ]
then	
	rm *.in
	exit 0
fi

evaluator='../groestl/groestl'
program='../Source/groestl'

version=$1
sum=0
fail=0

test_file_list=$(ls in*_$version.in 2>/dev/null)
test_no=$(ls in*_$version.in 2>/dev/null | wc -l)
echo $test_no

for file in $test_file_list
do
	sum=$(($sum+1))
	ev=$($evaluator $file $version 2>/dev/null)
	pr=$($program $file $version 2>/dev/null)
	#echo $ev
	#echo $pr
	echo "Test: $sum/$test_no"
	if [ "$ev" != "$pr" ]
	then
		echo "TEST FAIL: $file"
		fail=$(($fail+1))
	fi
done
echo "TESTING END"
echo "FAIL: $fail/$sum"
