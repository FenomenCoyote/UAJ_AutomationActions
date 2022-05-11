find . -type f -name *.wav -or -name *.mp3 -or -name *.ogg | while read file
do
	if [[$file!=./Assets/Audio/*]]
	then
		echo "ChSoundLocation: wrong sound file location - $file"
		exit 1
	fi
done
echo "ChSoundLocation: success"
exit 0