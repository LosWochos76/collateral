package de.hshl.visitor;

public class Hyperlink extends DocumentPart {
	protected String url;
	
	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public Hyperlink(String text, String url) {
		super(text);
		setUrl(url);
	}
	
   public void accept(DocumentConverterVisitor converter) {
      converter.visit(this);
   }
}
