
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF         =  0, // (EOF)
        SYMBOL_ERROR       =  1, // (Error)
        SYMBOL_WHITESPACE  =  2, // Whitespace
        SYMBOL_MINUS       =  3, // '-'
        SYMBOL_MINUSMINUS  =  4, // '--'
        SYMBOL_EXCLAMEQ    =  5, // '!='
        SYMBOL_PERCENT     =  6, // '%'
        SYMBOL_LPAREN      =  7, // '('
        SYMBOL_RPAREN      =  8, // ')'
        SYMBOL_TIMES       =  9, // '*'
        SYMBOL_TIMESTIMES  = 10, // '**'
        SYMBOL_DIV         = 11, // '/'
        SYMBOL_COLON       = 12, // ':'
        SYMBOL_SEMI        = 13, // ';'
        SYMBOL_LBRACE      = 14, // '{'
        SYMBOL_RBRACE      = 15, // '}'
        SYMBOL_PLUS        = 16, // '+'
        SYMBOL_PLUSPLUS    = 17, // '++'
        SYMBOL_LT          = 18, // '<'
        SYMBOL_EQ          = 19, // '='
        SYMBOL_EQEQ        = 20, // '=='
        SYMBOL_GT          = 21, // '>'
        SYMBOL_CASE        = 22, // case
        SYMBOL_DEFAULT     = 23, // default
        SYMBOL_DIGIT       = 24, // Digit
        SYMBOL_DO          = 25, // do
        SYMBOL_DOUBLE      = 26, // double
        SYMBOL_ELSE        = 27, // else
        SYMBOL_END         = 28, // End
        SYMBOL_FLOAT       = 29, // float
        SYMBOL_FOR         = 30, // for
        SYMBOL_ID          = 31, // Id
        SYMBOL_IF          = 32, // if
        SYMBOL_INT         = 33, // int
        SYMBOL_START       = 34, // Start
        SYMBOL_STRING      = 35, // string
        SYMBOL_SWITCH      = 36, // switch
        SYMBOL_WHILE       = 37, // while
        SYMBOL_ASSIGN      = 38, // <assign>
        SYMBOL_CASE2       = 39, // <case>
        SYMBOL_CASE_LIST   = 40, // <case_list>
        SYMBOL_CONCEPT     = 41, // <concept>
        SYMBOL_COND        = 42, // <cond>
        SYMBOL_DATA        = 43, // <data>
        SYMBOL_DIGIT2      = 44, // <digit>
        SYMBOL_EXP         = 45, // <exp>
        SYMBOL_EXPR        = 46, // <expr>
        SYMBOL_FACTOR      = 47, // <factor>
        SYMBOL_FOR_STMT    = 48, // <for_stmt>
        SYMBOL_ID2         = 49, // <id>
        SYMBOL_IF_STMT     = 50, // <if_stmt>
        SYMBOL_OP          = 51, // <op>
        SYMBOL_PROGRAM     = 52, // <program>
        SYMBOL_STEP        = 53, // <step>
        SYMBOL_STMT_LIST   = 54, // <stmt_list>
        SYMBOL_SWITCH_STMT = 55, // <switch_stmt>
        SYMBOL_TERM        = 56, // <term>
        SYMBOL_WHILE_STMT  = 57  // <while_stmt>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                                  =  0, // <program> ::= Start <stmt_list> End
        RULE_STMT_LIST                                          =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                         =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                            =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                           =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                           =  5, // <concept> ::= <for_stmt>
        RULE_CONCEPT4                                           =  6, // <concept> ::= <switch_stmt>
        RULE_CONCEPT5                                           =  7, // <concept> ::= <while_stmt>
        RULE_ASSIGN_EQ_SEMI                                     =  8, // <assign> ::= <id> '=' <expr> ';'
        RULE_ID_ID                                              =  9, // <id> ::= Id
        RULE_EXPR_PLUS                                          = 10, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                         = 11, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                               = 12, // <expr> ::= <term>
        RULE_TERM_TIMES                                         = 13, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                           = 14, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                       = 15, // <term> ::= <term> '%' <factor>
        RULE_TERM                                               = 16, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                  = 17, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                             = 18, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                  = 19, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                = 20, // <exp> ::= <id>
        RULE_EXP2                                               = 21, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                        = 22, // <digit> ::= Digit
        RULE_IF_STMT_IF_LPAREN_RPAREN_START_END                 = 23, // <if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End
        RULE_IF_STMT_IF_LPAREN_RPAREN_START_END_ELSE            = 24, // <if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End else <stmt_list>
        RULE_COND                                               = 25, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                              = 26, // <op> ::= '<'
        RULE_OP_GT                                              = 27, // <op> ::= '>'
        RULE_OP_EQEQ                                            = 28, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                        = 29, // <op> ::= '!='
        RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE = 30, // <for_stmt> ::= for '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stmt_list> '}'
        RULE_DATA_INT                                           = 31, // <data> ::= int
        RULE_DATA_FLOAT                                         = 32, // <data> ::= float
        RULE_DATA_DOUBLE                                        = 33, // <data> ::= double
        RULE_DATA_STRING                                        = 34, // <data> ::= string
        RULE_STEP_MINUSMINUS                                    = 35, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                   = 36, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                      = 37, // <step> ::= '++' <id>
        RULE_STEP_PLUSPLUS2                                     = 38, // <step> ::= <id> '++'
        RULE_STEP                                               = 39, // <step> ::= <assign>
        RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE     = 40, // <switch_stmt> ::= switch '(' <exp> ')' '{' <case_list> '}'
        RULE_CASE_LIST                                          = 41, // <case_list> ::= <case>
        RULE_CASE_LIST2                                         = 42, // <case_list> ::= <case_list> <case>
        RULE_CASE_CASE_COLON                                    = 43, // <case> ::= case <exp> ':' <stmt_list>
        RULE_CASE_DEFAULT_COLON                                 = 44, // <case> ::= default ':' <stmt_list>
        RULE_WHILE_STMT_WHILE_DO                                = 45  // <while_stmt> ::= while <exp> do <stmt_list>
    };

    public class MyParser
    {
        private LALRParser parser;

        ListBox lst;
        public MyParser(string filename, ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);

            this.lst = lst;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_LIST :
                //<case_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STMT :
                //<switch_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_STMT :
                //<while_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT5 :
                //<concept> ::= <while_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_SEMI :
                //<assign> ::= <id> '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_START_END :
                //<if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_START_END_ELSE :
                //<if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End else <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<for_stmt> ::= for '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE :
                //<switch_stmt> ::= switch '(' <exp> ')' '{' <case_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST :
                //<case_list> ::= <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST2 :
                //<case_list> ::= <case_list> <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_COLON :
                //<case> ::= case <exp> ':' <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_DEFAULT_COLON :
                //<case> ::= default ':' <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_STMT_WHILE_DO :
                //<while_stmt> ::= while <exp> do <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+ " In line : " + args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "Expected token: " + args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

    }
}
