myvar=0
height=1048576
while read file
do
	filesize=$(echo $file | xargs -I{} wc -c "{}" | awk '{print $1}')
	if [[ $filesize -gt $height ]]
	then
		echo "ChImageSize: $file exceeds allowed size"
		myvar=1
	fi
done <<< "$(find ./Assets/Images/ -type f -name *.png -or -name *.jpg -or -name *.jpeg)"
if [ $myvar == 0 ]
then
	echo "ChImageSize: success"
fi
exit $myvar
