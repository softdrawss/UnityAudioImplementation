# How can I implement sound in my buttons?
**BEFORE CONTINUING: It is rexcommended that first you check first the [volumes tutorial](..\volume\volume.md)**


1. Create a Game Object and add the script [AudioManager.cs](AudioManager.cs). If you already did it for other tutorials, there is no need to add it
2. Add the audio mixer

For the sound of each of the user's interactions, we need a sound for the hover, one for clicking regular buttons, and one for go back buttons.

You can add here an Audio Clip or a Random Audio Container.

3. Once you have the clicks, add them to the AudioManager
4. You can choose here the cooldown between hovers. You can edit this as you prefer.
5. Choose the output group for this sound. You can use, for example, the "SFX" group
6. Go to your Button and add the Script [ButtonSound.cs](ButtonSound.cs)
7. If it is a button to go back, select the checkbox "Is Back Button"