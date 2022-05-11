myvar=0
while read file
do
	if [[ $file != ./Assets/Scripts/* ]] && [[ $file != ./Assets/RunnerTesting/* ]]
	then
		echo "ChSourceLocation: wrong source file location - $file"
		myvar=1
	fi
done <<< "$(find . -type f -name *.cs)"
if [ $myvar == 0 ]
then
	echo "ChSourceLocation: success"
fi
exit $myvar
