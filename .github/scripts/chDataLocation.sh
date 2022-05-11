myvar=0
while read file
do
	if [[ $file != ./Assets/Data/* ]]
	then
		myvar=1
		echo "ChDataLocation: $file is not in Data folder"
	fi
done <<< "$(find . -type f -name *.json -or -name *.txt -or -name *.xaml -or -name *.xml -or -name *.csv)"
if [ $myvar == 0 ]
then
	echo "ChDataLocation: success"
fi
exit $myvar

