find . -type f -name *.png -or -name *.jpg -or -name *.jpeg | while read file
do
	if [[$file!=./Assets/Images/*]]
	then
		echo "ChImagesLocations: wrong file location - $file"
		exit 1
	fi
done
echo "ChImagesLocations: success"
exit 0