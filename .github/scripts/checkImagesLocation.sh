mistake=0

while read file
do
	if [[ $file != ./Assets/Images/* ]]
	then 
		echo "wrong file location at: $file"	
		mistake=1
	fi
done <<< "$(find . -type f -name *.png -or -name *.jpg -or -name *.jpeg)"

if [ $mistake == 0 ]; then
	echo "All is good"
fi

exit $mistake

