package com.mlkitmidlayer.textrecognition;

import com.google.mlkit.vision.common.InputImage;
import com.google.mlkit.vision.text.Text;
import com.google.mlkit.vision.text.TextRecognition;
import com.google.mlkit.vision.text.TextRecognizer;
import com.google.mlkit.vision.text.chinese.ChineseTextRecognizerOptions;
import com.google.mlkit.vision.text.devanagari.DevanagariTextRecognizerOptions;
import com.google.mlkit.vision.text.japanese.JapaneseTextRecognizerOptions;
import com.google.mlkit.vision.text.korean.KoreanTextRecognizerOptions;
import com.google.mlkit.vision.text.latin.TextRecognizerOptions;

import java.util.HashMap;
import java.util.Map;
import java.util.function.BiConsumer;

public class MLTextRecognition {
    private BiConsumer<Text, Integer> callbackAction;
    private final Map<String, TextRecognizer> textRecognizerInstances = new HashMap<>();

    private static volatile MLTextRecognition instance;

    public static MLTextRecognition getSharedInstance() {
        if (instance == null) {
            synchronized (MLTextRecognition.class) {
                if (instance == null) {
                    instance = new MLTextRecognition();
                }
            }
        }
        return instance;
    }

    private TextRecognizer textRecognizerInitialize(int scriptValue) {
        switch (scriptValue) {
            case 1:
                return TextRecognition.getClient(new ChineseTextRecognizerOptions.Builder().build());
            case 2:
                return TextRecognition.getClient(new DevanagariTextRecognizerOptions.Builder().build());
            case 3:
                return TextRecognition.getClient(new JapaneseTextRecognizerOptions.Builder().build());
            case 4:
                return TextRecognition.getClient(new KoreanTextRecognizerOptions.Builder().build());
            default:
                return TextRecognition.getClient(TextRecognizerOptions.DEFAULT_OPTIONS);
        }
    }

    public void closeTextRecognizerWithUid(String uid) {
        textRecognizerInstances.remove(uid);
    }

    public void startTextRecognizerWithInputs(String uid, int scriptValue,
                                              InputImage inputImage) {
        if (callbackAction == null) {
            return;
        }

        TextRecognizer textRecognizer = textRecognizerInstances.get(uid);
        if (textRecognizer == null) {
            textRecognizer = textRecognizerInitialize(scriptValue);
            textRecognizerInstances.put(uid, textRecognizer);
        }

        textRecognizer.process(inputImage)
                .addOnSuccessListener(visionText -> callbackAction.accept(visionText, 0))
                .addOnFailureListener(e -> {
                    System.out.println(e);
                    callbackAction.accept(null, 1);
                });
    }

    public void setCallbackAction(BiConsumer<Text, Integer> callbackAction) {
        this.callbackAction = callbackAction;
    }
}