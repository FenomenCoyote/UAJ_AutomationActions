mistake=0

find . -type f -name *.png -or -name *.jpg -or -name *.jpeg | while read file
do
	if [[ $file != ./Assets/Images/* ]] 
	then 
		echo "wrong file location at: $file"	
		mistake=1
	fi
done

if [ $mistake == 0 ]; then
	echo "All is good"
fi

exit $mistake

