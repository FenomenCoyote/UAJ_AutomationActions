mistake=0

find . -type f -name *.wav -or -name *.mp3 -or -name *.ogg | while read file
do
	if [[ $file != ./Assets/Audio/* ]] 
	then 
		echo "wrong file location at: $file"	
		mistake=1
	fi
done

if [ "$mistake" == 0 ]; then
	echo "All is good"
fi

exit $mistake

