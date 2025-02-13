namespace ArchReview.AgentAI;

public partial class Agent
{
    public static string GetPrompt(string fileTree) =>
        $"""
         INSTRUÇÕES:
            - Responda apenas com a classificação do resultdo, sem adicionar qualquer explanação ou detalhe.
            - Não descreva a estrutura de diretórios, contexto ou template arquitetural.
            - Classifique de acordo com as seguintes regras:
            
               1. 'MVC' quando os itens da [LISTA DE ARQUIVOS] CONTEM TODOS DIRETÓRIOS EM {ReadDataset(MVC)}.
               2. 'CLEAN' quando os itens da [LISTA DE ARQUIVOS] CONTEM TODOS DIRETÓRIOS EM {ReadDataset(CLEAN)} E NÃO CONTEM {ReadDataset(PORTS)}.
               3. 'VERTICAL' quando os itens da [LISTA DE ARQUIVOS] CONTEM ALGUM DIRETÓRIO EM {ReadDataset(VERTICAL)}.
               4. 'PORTS' quando os itens da [LISTA DE ARQUIVOS] CONTEM TODOS OS DIRETÓRIOS {ReadDataset(PORTS)}.
               5. Se nenhuma classificação for encontrada, classifique como 'AD-HOC'.

         RESULTADO ESPERADO:
            - 'RESULTADO: [CLASSIFICACAO]'   
            
         LISTA DE ARQUIVOS:
            {fileTree}
         """;


public static string GetPromptV2(string fileTree, string style, string dataset) =>
   $"""
    INSTRUÇÕES:
       - Responda apenas com a classificação do resultdo, sem adicionar qualquer explanação ou detalhe.
       - Não descreva a estrutura de diretórios, contexto ou template arquitetural.
       - Classifique de acordo com as seguintes regras:
       
          1. '{style}' quando os itens da [LISTA DE ARQUIVOS] CONTEM TODOS {ReadDataset(dataset)} E NÃO CONTEM {ReadDataset(CLEAN)}.
          2. 'CLEAN' quando os itens da [LISTA DE ARQUIVOS] CONTEM TODOS {ReadDataset(CLEAN)} E NÃO CONTEM {ReadDataset(PORTS)}.
          3. 'VERTICAL' quando os itens da [LISTA DE ARQUIVOS] CONTEM ALGUM {ReadDataset(VERTICAL)}.
          4. 'PORTS' quando os itens da [LISTA DE ARQUIVOS] CONTEM {ReadDataset(PORTS)}.
          5. Se nenhuma classificação for encontrada, classifique como 'AD-HOC'.

    RESULTADO ESPERADO:
       - 'RESULTADO: [CLASSIFICACAO]'   
       
    LISTA DE ARQUIVOS:
       {fileTree}
    """;
}