import random
import numpy as np
import nltk
from rapidfuzz import process
import re


class ChatBot:
    def __init__(self, executor, lemmatizer, stemmer, intents, vocabulary, model, training_data, words, classes):
        self.executor = executor
        self.lemmatizer = lemmatizer
        self.stemmer = stemmer
        self.intents = intents
        self.vocabulary = vocabulary
        self.model = model
        self.training_data = training_data
        self.words = words
        self.classes = classes

    def get_closest_word(self, word, vocabulary, threshold=70):
        # Using RapidFuzz's process module to find the closest match
        if word in vocabulary:  # Prioritize exact match
            return word
        closest_match = process.extractOne(word, vocabulary, score_cutoff=threshold)
        return closest_match[0] if closest_match else word

    def clean_up_sentence(self, sentence, vocab):
        sentence_words = nltk.word_tokenize(sentence)
        sentence_words = [self.lemmatizer.lemmatize(word.lower()) for word in sentence_words]
        sentence_words = [self.get_closest_word(word, vocab) for word in sentence_words]
        return sentence_words

    def bag_of_words(self, sentence):
        sentence_words = self.clean_up_sentence(sentence, self.vocabulary)
        bag = [0] * len(self.words)
        for s in sentence_words:
            for i, w in enumerate(self.words):
                if w == s:
                    bag[i] = 1
        return np.array(bag)

    def predict_class(self, sentence):
        bow = self.bag_of_words(sentence)
        res = self.model.predict(np.array([bow]))[0]
        ERROR_THRESHOLD = 0.90
        results = [[i, r] for i, r in enumerate(res) if r > ERROR_THRESHOLD]
        results.sort(key=lambda x: x[1], reverse=True)
        return_list = []
        for r in results:
            return_list.append({"intent": self.classes[r[0]], "probability": str(r[1])})
        return return_list

    def get_response(self, intents_list, intents_json):
        if intents_list:
            tag = intents_list[0]['intent']
            list_of_intents = intents_json['intents']
            for i in list_of_intents:
                if i['tag'] == tag:
                    return random.choice(i['responses'])
        else:
            return ""

    def chatbot_response(self, text):
        intents_list = self.predict_class(text)
        return self.get_response(intents_list, self.intents)

    def split_questions(self, user_question):
        # Create a regex pattern to match the delimiters
        # This pattern ensures that "და" and "ან" are only treated as delimiters when they are not part of another word
        pattern = r'[\?!.]+|[,;]+|(?<!\w)(?:და|ან|and|or|And)(?!\w)'  # Match ?, !, ., ,, ;, and only standalone და or ან
        questions = [q.strip() for q in re.split(pattern, user_question) if q.strip()]
        return questions

    def chat(self, question):
        user_question = question
        split_questions_list = self.split_questions(user_question)
        all_answers = []
        for question in split_questions_list:
            answer = self.chatbot_response(question)
            all_answers.append(answer) if answer != "" else []
        if not all_answers == []:
            final_answers = "\n\n".join(all_answers)
        else:
            final_answers = "Sorry! I don't have enough information about your question. Or there is some technical issues."
        return final_answers

