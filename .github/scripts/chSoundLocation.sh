myvar=0
while read file
do
	if [[ $file != ./Assets/Audio/* ]]
	then
		echo "ChSoundLocation: wrong sound file location - $file"
		myvar=1
	fi
done <<< "$(find . -type f -name *.mp3 -or -name *.wav -or -name *.ogg)"
if [ $myvar == 0 ]
then
	echo "ChSoundLocation: success"
fi
exit $myvar
