mistake=0
units=10
UNIT=MB

echo "Checking if any image is greater than $units $UNIT"

find ./Assets/Images -type f -size +10M | while read file
do
	fileSize=$(du -m $file | awk '{print $1;}')
	echo "Image $file with size $fileSize $UNIT is greater than $units $UNIT"
	mistake=1
done

if [ $mistake == 0 ]; then
	echo "All images are under $units $UNIT"
fi

exit $mistake

