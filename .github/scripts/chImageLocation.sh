myvar=0
while read file
do
	if [[ $file != ./Assets/Images/* ]]
	then
		echo "ChImagesLocations: wrong file location - $file" 
		myvar=1
	fi
done <<< "$(find . -type f -name *.png -or -name *.jpg -or -name *.jpeg)"
if [ $myvar == 0 ]
then
	echo "ChImagesLocations: success"
fi
exit $myvar
