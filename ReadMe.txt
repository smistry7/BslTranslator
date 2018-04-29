BSL Translator

To run a specific project go to the project folder's bin (debug or release) and run the .exe for that project 

Arff generator is the project used for adding hand data to the data sets

BslTranslator is a console application for translating the gestures using the pre written rules

RandomForestTranslator is a console application that outputs the predictions of the model based on the gestures being performed, 
if it is a low probability it will show 2 possible gestures.

BslTranslatorGUI is a winforms project that allows you to translate the gestures using either the data mining models or the
pre written rules. To add a gesture to the model this can be done by goint to the data mining tab and pressing the Add Gesture 
button. If recognition for a gesture in the Gesturelist is not of a high accuracy you can provide new training data by pressing 
its button in the data mining tab and following the instructions.

The DataSets Folder contains all the datasets with the various structures I experimented with and Models contains only the 
models used in the code.
