namespace Simple_Text_Editor_OOP;

public class TextProcessor
{
    private List<char[]> _savedText;
    private char[] _savedLine;
    private int _freeSpace;
    private int _rows = 1;
    private readonly List<Cursor> _ctrlz = new();
    private readonly List<Cursor> _redo = new();

    public TextProcessor(List<char[]> savedText, char[] savedLine)
    {
        _savedText = savedText;
        _savedLine = savedLine;
    }


    public void AddText(string? input)
    {
        var wordLenght = 0;
        if (input != null)
            while (input.Length + _freeSpace > _savedLine.Length)
                _savedLine = ExpandArray(_savedLine, input.Length + _freeSpace + _savedLine.Length);

        while (input != null && wordLenght != input.Length)
            for (var i = _freeSpace; i < input.Length + _freeSpace; i++)
            {
                _savedLine[i] = input[wordLenght];
                wordLenght++;
            }

        _freeSpace += input!.Length;
        if (_savedText.Count == 0)
            _savedText.Add(_savedLine);
        else
            _savedText[_rows - 1] = _savedLine;
    }

    private char[] ExpandArray(char[] original, int cols)
    {
        var newArray = new char[cols];
        for (var i = 0; i < original.Length; i++) newArray[i] = original[i];

        return newArray;
    }

    public void AddTextInside(string? input, int line, int column)
    {
        while (line > _rows)
        {
            _savedText.Add(new char[] { });
            _rows++;
            _freeSpace = 0;
        }

        var text = GetLine(_savedText, line - 1);
        if (input!.Length + _freeSpace > _savedText[line - 1].Length)
        {
            _savedText[line - 1] = ExpandArray(_savedText[line - 1], input.Length + _savedText[line - 1].Length);    
        }
        
        if (_savedText[line - 1][column] == '\0')
        {
            var wordLenght = 0;
            while (wordLenght != input.Length)
                for (var i = column; i < input.Length + column; i++)
                {
                    _savedText[line - 1][i] = input[wordLenght];
                    wordLenght++;
                    if (wordLenght == input.Length) break;
                }
        }
        else
        {
            var substrings = SplitAt(text, column);
            {
                var wordLenght = 0;
                while (wordLenght != input.Length)
                    for (var i = column; i < column + input.Length; i++)
                    {
                        _savedText[line - 1][i] = input[wordLenght];
                        wordLenght++;
                        if (wordLenght == input.Length) break;
                    }

                var counter1 = 0;
                for (var i = 0; i < column; i++)
                {
                    if (counter1 > substrings[0].Length) break;
                    _savedText[line - 1][i] = substrings[0][counter1];
                    counter1++;
                }

                var counter2 = 0;
                for (var i = column + input.Length; i < _savedText[line - 1].Length; i++)
                {
                    if (counter2 >= substrings[1].Length) break;
                    _savedText[line - 1][i] = substrings[1][counter2];
                    counter2++;
                }
            }
        }
    }

    public void Insert(int line, int index, string? text)
    {
        while (line > _rows)
        {
            _savedText.Add(new char[] { });
            _rows++;
            _freeSpace = 0;
        }

        var wordLenght = 0;
        while (text != null && wordLenght != text.Length)
            for (var i = index; i < text.Length + index; i++)
            {
                _savedText[line - 1][i] = text[wordLenght];
                wordLenght++;
                if (wordLenght == text.Length) break;
            }
    }

    public string SearchSubstring(string? substring)
    {
        var sameLetters = "";
        var substringFound = "";
        var occurrence = 0;
        var counter = 0;
        if (substring != null)
        {
            var substringArray = substring.ToArray();
            foreach (var line in _savedText)
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == substringArray[counter] && counter < substring.Length)
                    {
                        counter++;
                        sameLetters += line[i];
                    }

                    if (counter != 0 && counter <= substring.Length - 1)
                        if (line[i + 1] != substringArray[counter])
                        {
                            sameLetters = "";
                            counter = 0;
                        }

                    if (i + 1 >= line.Length && counter == 1)
                    {
                        substringFound += $"[{_savedText.IndexOf(line) - 1}] ";
                        counter = 0;
                        occurrence++;
                        sameLetters = "";
                    }

                    if (counter > substring.Length - 1 && i + 1 < line.Length)
                        if (line[i + 1] != substringArray[substring.Length - 1])
                        {
                            counter = 0;
                            if (sameLetters == substring)
                            {
                                substringFound += $"[{_savedText.IndexOf(line)}] ";
                                occurrence++;
                                sameLetters = "";
                            }
                        }
                }
        }

        return $"{occurrence} times at line: {substringFound}.";
    }

    private string GetLine(List<char[]> array, int line)
    {
        var text = "";
        for (var j = 0; j < array[line].Length; j++) text += array[line][j];

        return text;
    }

    public string GetText()
    {
        var text = "";
        for (var i = 0; i < _savedText.Count; i++)
        {
            if (i != 0) text += "\n";
            for (var j = 0; j < _savedText[i].Length; j++)
            {
                if (_savedText[i][j] == '\0')
                {
                    continue;
                }
                text += _savedText[i][j];
            }
        }

        return text;
    }

    private string[] SplitAt(string source, params int[] index)
    {
        index = index.Distinct().OrderBy(x => x).ToArray();
        var output = new string[index.Length + 1];
        var pos = 0;

        for (var i = 0; i < index.Length; pos = index[i++])
        {
            output[i] = source.Substring(pos, index[i] - pos);
        }

        output[index.Length] = source.Substring(pos);
        return output;
    }

    public void Delete(int line, int index, int length)
    {
        var deletedElement = "";
        var substrings = SplitAt(GetLine(_savedText, line - 1), index + length);
        for (int i = index; i < index + length; i++)
        {
            deletedElement += _savedText[line - 1][i];
            _savedText[line - 1][i] = ' ';
        }

        var pointer = new Cursor(line, index, deletedElement);
        _ctrlz.Add(pointer);
        _redo.Add(pointer);
        _savedText[line - 1] = new char[_savedText[line - 1].Length - index - length];
        var temporary = _freeSpace;
        if (index != 0)
        {
            var firstPart = substrings[0].Split($"{deletedElement}");
            _freeSpace = _savedText[line - 1].Length - length;
            AddTextInside(firstPart[0], line, 0);
            AddTextInside(substrings[1], line, firstPart[0].Length);
        }
        

        if (index == 0)
        {
            _freeSpace = _savedText[line - 1].Length - length;
            AddTextInside(substrings[1], line, 0);
        }
        _freeSpace = temporary;
    }

    public void Undo()
    {
        if (_ctrlz.Count == 0)
        {
            return;
        }

        if (_ctrlz.Count == 1)
        {
            AddTextInside(_ctrlz[0].GetText(), _ctrlz[0].GetLine(), _ctrlz[0].GetIndex());
            _ctrlz.Remove(_ctrlz[0]);
        }
        else
        {
            AddTextInside(_ctrlz[^1].GetText(), _ctrlz[^1].GetLine(), _ctrlz[^1].GetIndex());
            _ctrlz.Remove(_ctrlz[^1]);
        }
    }

    public void Redo()
    {
        if (_redo.Count == 0)
        {
            return;
        }

        if (_redo.Count == 1)
        {
            Delete(_redo[0].GetLine(), _redo[0].GetIndex(), _redo[0].GetText().Length);
            _redo.Remove(_redo[0]);
        }
        else
        {
            Delete(_redo[^1].GetLine(), _redo[^1].GetIndex(), _redo[^1].GetText().Length);
            _redo.Remove(_redo[^1]);
        }
    }

    public void CutArray(int line, int index, int symbolsLength)
    {
        Copy(line, index, symbolsLength);
        Delete(line, index, symbolsLength);
    }

    public void Paste(int line, int index)
    {
        AddTextInside(_ctrlz[^1].GetText(), line, index);
    }

    public void Copy(int line, int index, int symbolsLength)
    {
        var copiedElement = "";
        for (int i = index; i < index + symbolsLength; i++)
        {
            copiedElement += _savedText[line - 1][i];
        }

        var pointer = new Cursor(line, index, copiedElement);
        _ctrlz.Add(pointer);
    }

    public void LoadToMemory(string[] array)
    {
        var counter = 0;
        _rows = 1;
        while (counter != array.Length)
        {
            _savedText.Add(new char[] { });
            _savedLine = new char[array[counter].Length];
            AddText(array[counter]);
            counter++;
            _freeSpace = 0;
            _rows++;
        }

        _rows = array.Length;
        _freeSpace = array[_rows - 1].Length;
    }


    public void ClearText()
    {
        _savedText = new List<char[]>();
        _freeSpace = 0;
    }

    public void AddNewLine()
    {
        _rows++;
        _freeSpace = 0;
        _savedText.Add(new char[] { });
        _savedLine = new char[] { };
    }
}